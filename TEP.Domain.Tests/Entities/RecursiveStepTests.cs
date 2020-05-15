using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using TEP.Domain.Entities;
using TEP.Domain.Entities.Step;
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

        private readonly Interaction _keyInteraction;
        private readonly Interaction _keyholeInteraction;
        private readonly Interaction _doorInteraction;

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
            sequentialStep.CalculateDuration();
            // Assert
            Assert.AreEqual(3000, sequentialStep.ExpectedDuration.Milis);
            Assert.AreEqual(6000, sequentialStep.LimitDuration.Milis);
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
            Assert.AreEqual(fourthTime.Millisecond - firstTime.Millisecond, sequentialStep.ExecutionTime.Milis);
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
            //Assert.AreEqual(null, sequentialStep.CurrentStep);
            Assert.AreEqual(true, sequentialStep.Completed);
           // Assert.AreEqual(false, sequentialStep.Active);
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

        //tetnar avançar ja tendo acabado lança excessão

        //avançar sem subpassos laça excessão

        /*[TestMethod]
        public void CorrectMultiLevelStep...()
        {

        }*/

    }
}
