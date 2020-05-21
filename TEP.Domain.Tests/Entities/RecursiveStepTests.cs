using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using TEP.Domain.Entities;
using TEP.Domain.ValueObjects;
using TEP.Shared.ValueObjects;

namespace TEP.Domain.Tests.Entities
{
    [TestClass]
    public class RecursiveStepTests
    {
        private readonly LeafStep _takeKeyStep;
        private readonly LeafStep _insertKeyStep;
        private readonly LeafStep _openDoorStep;
        private readonly LeafStep _grabHugStep;

        private readonly Interaction _keyInteraction;
        private readonly Interaction _keyholeInteraction;
        private readonly Interaction _doorInteraction;
        private readonly Interaction _grabHugInteraction;

        public RecursiveStepTests()
        {
            List<Category> takeKeyCategories = new List<Category>();
            takeKeyCategories.Add(Category.Operational);
            Description takeKeyDescription = new Description("Take the Key.");
            Duration takeKeyExpected = new Duration(1000);
            Duration takeKeyLimit = new Duration(2000);
            _keyInteraction = new Interaction(takeKeyCategories, Act.Grab, takeKeyDescription, takeKeyExpected, takeKeyLimit);
            _takeKeyStep = new LeafStep(Standard.Mandatory, "Taking Key", _keyInteraction);

            List<Category> insertKeyCategories = new List<Category>();
            insertKeyCategories.Add(Category.Operational);
            Description insertKeyDescription = new Description("Insert key into door.");
            Duration insertKeyExpected = new Duration(1000);
            Duration insertKeyLimit = new Duration(2000);
            _keyholeInteraction = new Interaction(insertKeyCategories, Act.Grab, insertKeyDescription, insertKeyExpected, insertKeyLimit);
            _insertKeyStep = new LeafStep(Standard.Mandatory, "Insert key.", _keyholeInteraction);

            List<Category> openDoorCategories = new List<Category>();
            openDoorCategories.Add(Category.Operational);
            Description openDoorDescription = new Description("Open door.");
            Duration openDoorExpected = new Duration(1000);
            Duration openDoorLimit = new Duration(2000);
            _doorInteraction = new Interaction(openDoorCategories, Act.Grab, openDoorDescription, openDoorExpected, openDoorLimit);
            _openDoorStep = new LeafStep(Standard.Mandatory, "Insert key.", _doorInteraction);

            List<Category> grabHugCategories = new List<Category>();
            grabHugCategories.Add(Category.Operational);
            grabHugCategories.Add(Category.Security);
            Description grabHugDescription = new Description("Grab HUg.");
            Duration grabHugExpected = new Duration(1000);
            Duration grabHugLimit = new Duration(2000);
            _grabHugInteraction = new Interaction(grabHugCategories, Act.Grab, grabHugDescription, grabHugExpected, grabHugLimit);
            _grabHugStep = new LeafStep(Standard.Mandatory, "Insert key.", _grabHugInteraction);
        }

        [TestMethod]
        public void OnInitialStateShouldBeNonActiveAndNonCompleted()
        {
            // Arrange            
            List<Step> steps = new List<Step>();
            RecursiveStep sequentialStep = new RecursiveStep(Standard.Mandatory, "Door", steps);
            sequentialStep.AddSubStep(_takeKeyStep);
            sequentialStep.AddSubStep(_insertKeyStep);
            sequentialStep.AddSubStep(_openDoorStep);

            // Assert
            Assert.AreEqual(false, sequentialStep.Active);
            Assert.AreEqual(false, sequentialStep.Completed);
        }
        [TestMethod]
        public void OnInitialStateExecutionAndLimitTimeShouldBeCorrectCalculated()
        {
            // Arrange            
            List<Step> steps = new List<Step>();
            RecursiveStep sequentialStep = new RecursiveStep(Standard.Mandatory, "Door", steps);
            sequentialStep.AddSubStep(_takeKeyStep);
            sequentialStep.AddSubStep(_insertKeyStep);
            sequentialStep.AddSubStep(_openDoorStep);
            sequentialStep.UpdateDuration();
            // Assert
            Assert.AreEqual(3000, sequentialStep.ExpectedDuration.Seconds);
            Assert.AreEqual(6000, sequentialStep.LimitDuration.Seconds);
        }
        [TestMethod]
        public void OnCompletionExecutionTimeShouldBeCalculated()
        {
            // Arrange            
            List<Step> steps = new List<Step>();
            RecursiveStep sequentialStep = new RecursiveStep(Standard.Mandatory, "Door", steps);
            sequentialStep.AddSubStep(_takeKeyStep);
            sequentialStep.AddSubStep(_insertKeyStep);
            sequentialStep.AddSubStep(_openDoorStep);
            DateTime firstTime = DateTime.Now;
            DateTime secondTime = DateTime.Now;
            DateTime thirdTime = DateTime.Now;
            DateTime fourthTime = DateTime.Now;
            // Act
            sequentialStep.AdvanceStep(firstTime);
            sequentialStep.AdvanceStep(secondTime);
            sequentialStep.AdvanceStep(thirdTime);
            sequentialStep.AdvanceStep(fourthTime);
            // Assert
            Assert.AreEqual(fourthTime.Subtract(firstTime).TotalSeconds, sequentialStep.ExecutionTime.Seconds);
        }
        [TestMethod]
        public void OnCompletionShouldReturnNullAsInteractionBeCompletedAndNonActive()
        {
            // Arrange            
            List<Step> steps = new List<Step>();
            RecursiveStep sequentialStep = new RecursiveStep(Standard.Mandatory, "Door", steps);
            sequentialStep.AddSubStep(_takeKeyStep);
            sequentialStep.AddSubStep(_insertKeyStep);
            sequentialStep.AddSubStep(_openDoorStep);
            DateTime firstTime = DateTime.Now;
            DateTime secondTime = DateTime.Now;
            DateTime thirdTime = DateTime.Now;
            DateTime fourthTime = DateTime.Now;
            // Act
            sequentialStep.AdvanceStep(firstTime);
            sequentialStep.AdvanceStep(secondTime);
            sequentialStep.AdvanceStep(thirdTime);
            sequentialStep.AdvanceStep(fourthTime);
            // Assert
            Assert.AreEqual(null, sequentialStep.CurrentSubStep);
            Assert.AreEqual(true, sequentialStep.Completed);
            Assert.AreEqual(false, sequentialStep.Active);
        }
        [TestMethod]
        public void OnActivationShouldReturnFirstInteractionBeActiveAndNonCompleted()
        {
            // Arrange            
            List<Step> steps = new List<Step>();
            RecursiveStep sequentialStep = new RecursiveStep(Standard.Mandatory, "Door", steps);
            sequentialStep.AddSubStep(_takeKeyStep);
            sequentialStep.AddSubStep(_insertKeyStep);
            sequentialStep.AddSubStep(_openDoorStep);
            DateTime firstTime = DateTime.Now;
            // Act
            Interaction first = sequentialStep.AdvanceStep(firstTime).Interaction;
            // Assert
            Assert.AreEqual(_keyInteraction, first);
            Assert.AreEqual(false, sequentialStep.Completed);
            Assert.AreEqual(true, sequentialStep.Active);
        }
        [TestMethod]
        public void OnSecondAdvanceShouoldReturnSecondInteractionAndBeActiveAndNonCompelted()
        {
            // Arrange            
            List<Step> steps = new List<Step>();
            RecursiveStep sequentialStep = new RecursiveStep(Standard.Mandatory, "Door", steps);
            sequentialStep.AddSubStep(_takeKeyStep);
            sequentialStep.AddSubStep(_insertKeyStep);
            sequentialStep.AddSubStep(_openDoorStep);
            DateTime firstTime = DateTime.Now;
            DateTime secondTime = DateTime.Now;
            // Act
            sequentialStep.AdvanceStep(firstTime);
            Interaction second = sequentialStep.AdvanceStep(secondTime).Interaction;
            // Assert
            Assert.AreEqual(_keyholeInteraction, second);
            Assert.AreEqual(false, sequentialStep.Completed);
            Assert.AreEqual(true, sequentialStep.Active);
        }
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException), "This step has already been completed. Can't perform it again.")]
        public void OnCompletedAdvancingStepShouldFail()
        {
            // Arrange            
            List<Step> steps = new List<Step>();
            RecursiveStep sequentialStep = new RecursiveStep(Standard.Mandatory, "Door", steps);
            sequentialStep.AddSubStep(_takeKeyStep);
            sequentialStep.AddSubStep(_insertKeyStep);
            sequentialStep.AddSubStep(_openDoorStep);
            DateTime firstTime = DateTime.Now;
            DateTime secondTime = DateTime.Now;
            DateTime thirdTime = DateTime.Now;
            DateTime fourthTime = DateTime.Now;
            DateTime fifthTime = DateTime.Now;
            // Act
            sequentialStep.AdvanceStep(firstTime);  //starts
            sequentialStep.AdvanceStep(secondTime); //finishes first and starts second
            sequentialStep.AdvanceStep(thirdTime);  //finishes second and starts third
            sequentialStep.AdvanceStep(fourthTime); //finishes third and completes
            sequentialStep.AdvanceStep(fifthTime);  //exception            
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void OnEmptySubStepsShouldThrowException()
        {
            // Arrange            
            List<Step> steps = new List<Step>();
            RecursiveStep sequentialStep = new RecursiveStep(Standard.Mandatory, "Door", steps);
            DateTime firstTime = DateTime.Now;
            // Act
            sequentialStep.AdvanceStep(firstTime);  //starts        
        }
        [TestMethod]
        public void OnCompletionMultinivelSequentialStepsShouldBeCompletedAndNonActive()
        {
            // Arrange    
            List<Step> hugSteps = new List<Step>();
            RecursiveStep hugSequentialStep = new RecursiveStep(Standard.Mandatory, "Hug.", hugSteps);
            hugSequentialStep.AddSubStep(_grabHugStep);
            hugSequentialStep.AddSubStep(_takeKeyStep);

            List<Step> doorSteps = new List<Step>();
            RecursiveStep doorSequentialStep = new RecursiveStep(Standard.Mandatory, "Door", doorSteps);
            doorSequentialStep.AddSubStep(_insertKeyStep);
            doorSequentialStep.AddSubStep(_openDoorStep);

            List<Step> multiNivelSequentialSteps = new List<Step>();
            RecursiveStep multiNivelSequentialStep = new RecursiveStep(Standard.Mandatory, "Unlock Door", multiNivelSequentialSteps);
            multiNivelSequentialStep.AddSubStep(hugSequentialStep);
            multiNivelSequentialStep.AddSubStep(doorSequentialStep);

            // Act
            multiNivelSequentialStep.AdvanceStep(DateTime.Now);
            multiNivelSequentialStep.AdvanceStep(DateTime.Now);
            multiNivelSequentialStep.AdvanceStep(DateTime.Now);
            multiNivelSequentialStep.AdvanceStep(DateTime.Now);
            multiNivelSequentialStep.AdvanceStep(DateTime.Now);

            // Assert
            Assert.AreEqual(true, multiNivelSequentialStep.Completed);
            Assert.AreEqual(false, multiNivelSequentialStep.Active);
        }
        [TestMethod]
        public void OnCompletionAndCalculationAllTimesShouldBeCorrect()
        {
            // Arrange    
            List<Step> hugSteps = new List<Step>();
            RecursiveStep hugSequentialStep = new RecursiveStep(Standard.Mandatory, "Hug.", hugSteps);
            hugSequentialStep.AddSubStep(_grabHugStep);
            hugSequentialStep.AddSubStep(_takeKeyStep);

            List<Step> doorSteps = new List<Step>();
            RecursiveStep doorSequentialStep = new RecursiveStep(Standard.Mandatory, "Door", doorSteps);
            doorSequentialStep.AddSubStep(_insertKeyStep);
            doorSequentialStep.AddSubStep(_openDoorStep);

            List<Step> multiNivelSequentialSteps = new List<Step>();
            RecursiveStep multiNivelSequentialStep = new RecursiveStep(Standard.Mandatory, "Unlock Door", multiNivelSequentialSteps);
            multiNivelSequentialStep.AddSubStep(hugSequentialStep);
            multiNivelSequentialStep.AddSubStep(doorSequentialStep);

            DateTime firstTime = DateTime.Now;
            DateTime secondTime = DateTime.Now;
            DateTime thirdTime = DateTime.Now;
            DateTime fourthTime = DateTime.Now;
            DateTime fifthTime = DateTime.Now;

            // Act
            multiNivelSequentialStep.UpdateDuration();
            multiNivelSequentialStep.AdvanceStep(firstTime);
            multiNivelSequentialStep.AdvanceStep(secondTime);
            multiNivelSequentialStep.AdvanceStep(thirdTime);
            System.Threading.Thread.Sleep(2000);
            multiNivelSequentialStep.AdvanceStep(fourthTime);
            multiNivelSequentialStep.AdvanceStep(fifthTime);

            // Assert
            Assert.AreEqual(4000, multiNivelSequentialStep.ExpectedDuration.Seconds);
            Assert.AreEqual(8000, multiNivelSequentialStep.LimitDuration.Seconds);
            var timeOffset = fifthTime.Subtract(firstTime).TotalSeconds;
            Assert.AreEqual((long)timeOffset, (long)multiNivelSequentialStep.ExecutionTime.Seconds);
        }
        [TestMethod]
        public void OnTwoAdvancesMultinivelSequentialStepsShouldBeNonCompletedActiveAndReturnSecondInteraction()
        {
            // Arrange    
            List<Step> hugSteps = new List<Step>();
            RecursiveStep hugSequentialStep = new RecursiveStep(Standard.Mandatory, "Hug.", hugSteps);
            hugSequentialStep.AddSubStep(_grabHugStep);
            hugSequentialStep.AddSubStep(_takeKeyStep);

            List<Step> doorSteps = new List<Step>();
            RecursiveStep doorSequentialStep = new RecursiveStep(Standard.Mandatory, "Door", doorSteps);
            doorSequentialStep.AddSubStep(_insertKeyStep);
            doorSequentialStep.AddSubStep(_openDoorStep);

            List<Step> multiNivelSequentialSteps = new List<Step>();
            RecursiveStep multiNivelSequentialStep = new RecursiveStep(Standard.Mandatory, "Unlock Door", multiNivelSequentialSteps);
            multiNivelSequentialStep.AddSubStep(hugSequentialStep);
            multiNivelSequentialStep.AddSubStep(doorSequentialStep);

            // Act
            multiNivelSequentialStep.AdvanceStep(DateTime.Now);
            LeafStep second = multiNivelSequentialStep.AdvanceStep(DateTime.Now);

            // Assert
            Assert.AreEqual(false, multiNivelSequentialStep.Completed);
            Assert.AreEqual(true, multiNivelSequentialStep.Active);
            Assert.AreEqual(_takeKeyStep.Interaction, second.Interaction);
        }
        [TestMethod]
        public void OnThreeAdvancesShouldChangeActiveRecursiveSubStepAndReturnThirdInteraction()
        {
            // Arrange    
            List<Step> hugSteps = new List<Step>();
            RecursiveStep hugSequentialStep = new RecursiveStep(Standard.Mandatory, "Hug.", hugSteps);
            hugSequentialStep.AddSubStep(_grabHugStep);
            hugSequentialStep.AddSubStep(_takeKeyStep);

            List<Step> doorSteps = new List<Step>();
            RecursiveStep doorSequentialStep = new RecursiveStep(Standard.Mandatory, "Door", doorSteps);
            doorSequentialStep.AddSubStep(_insertKeyStep);
            doorSequentialStep.AddSubStep(_openDoorStep);

            List<Step> multiNivelSequentialSteps = new List<Step>();
            RecursiveStep multiNivelSequentialStep = new RecursiveStep(Standard.Mandatory, "Unlock Door", multiNivelSequentialSteps);
            multiNivelSequentialStep.AddSubStep(hugSequentialStep);
            multiNivelSequentialStep.AddSubStep(doorSequentialStep);

            // Act
            multiNivelSequentialStep.AdvanceStep(DateTime.Now);
            multiNivelSequentialStep.AdvanceStep(DateTime.Now);
            LeafStep third = multiNivelSequentialStep.AdvanceStep(DateTime.Now);

            // Assert
            Assert.AreEqual(true, hugSequentialStep.Completed);
            Assert.AreEqual(false, hugSequentialStep.Active);
            Assert.AreEqual(false, doorSequentialStep.Completed);
            Assert.AreEqual(true, doorSequentialStep.Active);
            Assert.AreEqual(_insertKeyStep.Interaction, third.Interaction);
            Assert.AreEqual(doorSequentialStep, multiNivelSequentialStep.CurrentSubStep);
        }
    }
}
