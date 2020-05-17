using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using TEP.Domain.Entities;
using TEP.Domain.Entities.StepEntities;
using TEP.Domain.ValueObjects;
using TEP.Shared;
using TEP.Shared.ValueObjects;

namespace TEP.Domain.Tests
{
    [TestClass]
    public class ProcedureTests : Setup
    {

        [TestMethod]
        public void OnToJsonReturnsJSONString()
        {

        }
        [TestMethod]
        public void OnToJsonWithInconsistentJsonThrowsError()
        {

        }
        [TestMethod]
        public void OnFromJsonFullFillsTheProcedureWithSteps()
        {

        }
        [TestMethod]
        public void OnFromJsonWithInconsistentJsonThrowsError()
        {

        }
        [TestMethod]
        public void OnGettingCurrentInteractionReturnsCorretInteraction()
        {

        }
        [TestMethod]
        public void OnGettingCurrentReturnsNullIfCompleted()
        {

        }
        [TestMethod]
        public void OnNextInteractionReturnsCorretInteraction()
        {

        }
        [TestMethod]
        public void OnNextInteractionReturnsNullIfItWasLastInteraction()
        {

        }
        [TestMethod]
        public void OnNextInteractionAfterCompletionThrowsException()
        {

        }
        [TestMethod]
        public void OnUpdateTimeWithouStartingExecutionTimesAreCorrect()
        {

        }
        [TestMethod]
        public void OnUpdateTimeInMiddleOfExecutionTimesAreCorrect()
        {

        }
        [TestMethod]
        public void OnUpdateTimeAfterExecutionTimesAreCorrect()
        {

        }   
        [TestMethod]
        public void OnCallRequiredAssets_ShoulReturnListWithNonDuplicatedAssets()
        {
            //Arrange
            var procedure = new Procedure(new Description("Door Oppening"), "Door 01", _MultinivelStep);
            //Act
            List<IAsset> assets = procedure.RequiredAssets();
            //Assert
            Assert.AreEqual(3, assets.Count);
        }
    }
}
