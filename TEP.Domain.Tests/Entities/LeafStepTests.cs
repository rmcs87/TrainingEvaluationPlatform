using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using TEP.Domain.Entities;
using TEP.Domain.Entities.StepEntities;
using TEP.Domain.ValueObjects;
using TEP.Shared.ValueObjects;

namespace TEP.Domain.Tests.Entities
{
    [TestClass]
    public class LeafStepTests
    {
        [TestMethod]
        public void CorrectInitialStatsForLeafStep()
        {
            // Arrange
            List<Category> categories = new List<Category>();
            categories.Add(Category.Operational);
            Description description = new Description("Take the Key.");
            Duration expected = new Duration(1000);
            Duration limit = new Duration(2000);
            Interaction interaction = new Interaction(categories, Act.Grab, description, expected, limit);
            LeafStep leafStep = new LeafStep(Standard.Mandatory, "Taking Key", interaction);
            // Act
 
            // Assert
            Assert.AreEqual(false, leafStep.Active);
            Assert.AreEqual(false, leafStep.Completed);
        }
        [TestMethod]
        public void CorrectAdvanceStepForNonActiveNonCompletedStep()
        {
            // Arrange
            List<Category> categories = new List<Category>();
            categories.Add(Category.Operational);
            Description description = new Description("Take the Key.");
            Duration expected = new Duration(1000);
            Duration limit = new Duration(2000);
            Interaction interaction = new Interaction(categories, Act.Grab, description, expected, limit);
            LeafStep leafStep = new LeafStep(Standard.Mandatory, "Taking Key", interaction);
            // Act
            leafStep.AdvanceStep(DateTime.Now);
            // Assert
            Assert.AreEqual(true, leafStep.Active);
            Assert.AreEqual(false, leafStep.Completed);
        }
        [TestMethod]
        public void CorrectAdvanceStepForActiveNonCompletedStep()
        {
            // Arrange
            List<Category> categories = new List<Category>();
            categories.Add(Category.Operational);
            Description description = new Description("Take the Key.");
            Duration expected = new Duration(1000);
            Duration limit = new Duration(2000);
            Interaction interaction = new Interaction(categories, Act.Grab, description, expected, limit);
            LeafStep leafStep = new LeafStep(Standard.Mandatory, "Taking Key", interaction);
            // Act
            leafStep.AdvanceStep(DateTime.Now);
            leafStep.AdvanceStep(DateTime.Now);
            // Assert
            Assert.AreEqual(false, leafStep.Active);
            Assert.AreEqual(true, leafStep.Completed);
        }
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException), "This step has already been completed. Can't perform it again.")]
        public void CorrectAdvanceStepForCompletedStep()
        {
            // Arrange
            List<Category> categories = new List<Category>();
            categories.Add(Category.Operational);
            Description description = new Description("Take the Key.");
            Duration expected = new Duration(1000);
            Duration limit = new Duration(2000);
            Interaction interaction = new Interaction(categories, Act.Grab, description, expected, limit);
            LeafStep leafStep = new LeafStep(Standard.Mandatory, "Taking Key", interaction);
            // Act
            leafStep.AdvanceStep(DateTime.Now);
            leafStep.AdvanceStep(DateTime.Now);
            leafStep.AdvanceStep(DateTime.Now);
        }
        [TestMethod]
        public void CorrectExecutionTimeOnStart()
        {
            // Arrange
            List<Category> categories = new List<Category>();
            categories.Add(Category.Operational);
            Description description = new Description("Take the Key.");
            Duration expected = new Duration(1000);
            Duration limit = new Duration(2000);
            Interaction interaction = new Interaction(categories, Act.Grab, description, expected, limit);
            LeafStep leafStep = new LeafStep(Standard.Mandatory, "Taking Key", interaction);
            // Act

            // Assert
            Assert.AreEqual(0 , leafStep.ExecutionTime.Seconds);
        }
        [TestMethod]
        public void CorrectExecutionTimeOnCompletion()
        {
            // Arrange
            List<Category> categories = new List<Category>();
            categories.Add(Category.Operational);
            Description description = new Description("Take the Key.");
            Duration expected = new Duration(1000);
            Duration limit = new Duration(2000);
            Interaction interaction = new Interaction(categories, Act.Grab, description, expected, limit);
            LeafStep leafStep = new LeafStep(Standard.Mandatory, "Taking Key", interaction);
            DateTime firstTime = DateTime.Now;
            DateTime secondTime = DateTime.Now;
            // Act
            leafStep.AdvanceStep(firstTime);
            leafStep.AdvanceStep(secondTime);
            // Assert
            Assert.AreEqual(secondTime.Subtract(firstTime).TotalSeconds, leafStep.ExecutionTime.Seconds);
        }
        [TestMethod]
        public void ExpectedTime_for_Interaction()
        {
            // Arrange
            List<Category> categories = new List<Category>();
            categories.Add(Category.Operational);
            Description description = new Description("Take the Key.");
            Duration expected = new Duration(1000);
            Duration limit = new Duration(2000);
            Interaction interaction = new Interaction(categories, Act.Grab, description, expected, limit);
            LeafStep leafStep = new LeafStep(Standard.Mandatory, "Taking Key", interaction);
            // Act
            leafStep.UpdateDuration();
            // Assert
            Assert.AreEqual(expected.Seconds, leafStep.ExpectedDuration.Seconds);
        }       
        [TestMethod]
        public void LimitTime_for_Interaction()
        {
            // Arrange
            List<Category> categories = new List<Category>();
            categories.Add(Category.Operational);
            Description description = new Description("Take the Key.");
            Duration expected = new Duration(1000);
            Duration limit = new Duration(2000);
            Interaction interaction = new Interaction(categories, Act.Grab, description, expected, limit);
            LeafStep leafStep = new LeafStep(Standard.Mandatory, "Taking Key", interaction);
            // Act
            leafStep.UpdateDuration();
            // Assert
            Assert.AreEqual(limit.Seconds, leafStep.LimitDuration.Seconds);
        }
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException), "ProcessDuration must be executed before getting Time.")]
        public void ErrorOnGettingDurationWithoutProcessing()
        {
            // Arrange
            List<Category> categories = new List<Category>();
            categories.Add(Category.Operational);
            Description description = new Description("Take the Key.");
            Duration expected = new Duration(20000);
            Duration limit = new Duration(30000);
            Interaction interaction = new Interaction(categories, Act.Grab, description, expected, limit);
            LeafStep leafStep = new LeafStep(Standard.Mandatory, "Taking Key", interaction);
            /// Act
            var millis = leafStep.ExpectedDuration.Seconds;
        }
       
    }

}
