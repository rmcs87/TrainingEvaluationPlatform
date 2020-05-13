using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using TEP.Domain.Entities;
using TEP.Domain.Entities.Step;
using TEP.Domain.ValueObjects;
using TEP.Shared.ValueObjects;

namespace TEP.Domain.Tests.Entities
{
    [TestClass]
    public class LeafStepTests
    {
        [TestMethod]
        public void ExpeectedTime_for_Interaction()
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
            leafStep.ProcessDuration();
            // Assert
            Assert.AreEqual(expected.Milis, leafStep.ExpectedDuration.Milis);
        }       
        [TestMethod]
        public void LimitTime_for_Interaction()
        {
            // Arrange
            List<Category> categories = new List<Category>();
            categories.Add(Category.Operational);
            Description description = new Description("Take the Key.");
            Duration expected = new Duration(1000.0f);
            Duration limit = new Duration(2000.0f);
            Interaction interaction = new Interaction(categories, Act.Grab, description, expected, limit);
            LeafStep leafStep = new LeafStep(Standard.Mandatory, "Taking Key", interaction);
            // Act
            leafStep.ProcessDuration();
            // Assert
            Assert.AreEqual(limit.Milis, leafStep.LimitDuration.Milis);
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
            var millis = leafStep.ExpectedDuration.Milis;
        }
       
    }

}
