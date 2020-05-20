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
        public void OnGettingCurrentInteraction_ReturnsCorretInteraction_OnMiddleStep()
        {
            //Arrange
            var procedure = new Procedure(new Description("Door Oppening"), "Door 01", _MultinivelStep);
            DateTime firstTime = DateTime.Now;
            DateTime secondTime = DateTime.Now;
            DateTime thirdTime = DateTime.Now;
            DateTime fourthTime = DateTime.Now;
            DateTime fifthTime = DateTime.Now;
            //Act
            procedure.NextInteraction(firstTime);
            procedure.NextInteraction(secondTime);

            Interaction currentInteraction = procedure.GetCurrentInteraction();
            //Assert
            Assert.AreEqual(_keyInteraction, currentInteraction);
        }
        [TestMethod]
        public void OnGettingCurrentInteraction_ReturnsNull_IfCompleted()
        {
            //Arrange
            var procedure = new Procedure(new Description("Door Oppening"), "Door 01", _MultinivelStep);
            DateTime firstTime = DateTime.Now;
            DateTime secondTime = DateTime.Now;
            DateTime thirdTime = DateTime.Now;
            DateTime fourthTime = DateTime.Now;
            DateTime fifthTime = DateTime.Now;
            //Act
            procedure.NextInteraction(firstTime);
            Interaction secondInteractions = procedure.NextInteraction(secondTime);
            procedure.NextInteraction(thirdTime);
            procedure.NextInteraction(fourthTime);
            procedure.NextInteraction(fifthTime);

            Interaction currentInteraction = procedure.GetCurrentInteraction();
            //Assert
            Assert.AreEqual(null, currentInteraction);
        }
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException), "This Procedure has not Been started. Call Procedure.NextInteraction to start.")]
        public void OnGettingCurrentInteraction_ThrowsException_IfNotStarted()
        {
            //Arrange
            var procedure = new Procedure(new Description("Door Oppening"), "Door 01", _MultinivelStep);
            //Act            
            Interaction currentInteraction = procedure.GetCurrentInteraction();
            //Assert
           
        }
        [TestMethod]
        public void OnCallNextInteraction_IfNotCompletedNorLastInteraction_ReturnsCorretInteraction()
        {
            //Arrange
            var procedure = new Procedure(new Description("Door Oppening"), "Door 01", _MultinivelStep);
            DateTime firstTime = DateTime.Now;
            DateTime secondTime = DateTime.Now;
            DateTime thirdTime = DateTime.Now;
            DateTime fourthTime = DateTime.Now;
            DateTime fifthTime = DateTime.Now;
            //Act
            procedure.NextInteraction(firstTime);
            Interaction secondInteractions = procedure.NextInteraction(secondTime);
            procedure.NextInteraction(thirdTime);
            procedure.NextInteraction(fourthTime);
            procedure.NextInteraction(fifthTime);
            //Assert
            Assert.AreEqual(_keyInteraction, secondInteractions);
        }
        [TestMethod]
        public void OnCallNextInteraction_IfLastInteraction_ReturnsNull()
        {
            //Arrange
            var procedure = new Procedure(new Description("Door Oppening"), "Door 01", _MultinivelStep);
            DateTime firstTime = DateTime.Now;
            DateTime secondTime = DateTime.Now;
            DateTime thirdTime = DateTime.Now;
            DateTime fourthTime = DateTime.Now;
            DateTime fifthTime = DateTime.Now;
            //Act
            procedure.NextInteraction(firstTime);
            procedure.NextInteraction(secondTime);
            procedure.NextInteraction(thirdTime);
            procedure.NextInteraction(fourthTime);
            Interaction lastInteractions = procedure.NextInteraction(fifthTime);
            //Assert
            Assert.AreEqual(null, lastInteractions);
        }
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException), "This Procedure has already been completed. Can't perform it again.")]
        public void OnCallNextInteraction_AfterCompletion_ThrowsException()
        {
            //Arrange
            var procedure = new Procedure(new Description("Door Oppening"), "Door 01", _MultinivelStep);
            DateTime firstTime = DateTime.Now;
            DateTime secondTime = DateTime.Now;
            DateTime thirdTime = DateTime.Now;
            DateTime fourthTime = DateTime.Now;
            DateTime fifthTime = DateTime.Now;
            DateTime sixthTime = DateTime.Now;
            //Act
            procedure.NextInteraction(firstTime);
            procedure.NextInteraction(secondTime);
            procedure.NextInteraction(thirdTime);
            procedure.NextInteraction(fourthTime);
            procedure.NextInteraction(fifthTime);
            procedure.NextInteraction(sixthTime);
            //Assert
        }
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException), "ProcessDuration must be executed before getting Time.")]
        public void OnCreation_GetingTimesWithoutProcessing_ShouldThrowException()
        {
            //Arrange
            var procedure = new Procedure(new Description("Door Oppening"), "Door 01", _MultinivelStep);
            //Act

            //Assert
            Assert.AreEqual(0, procedure.Execution.Seconds);
            Assert.AreEqual(0, procedure.Expected.Seconds);
            Assert.AreEqual(0, procedure.Limit.Seconds);
        }
        [TestMethod]
        public void OnCreation_GetingTimesAfterProcessing_ShouldThrowException()
        {
            //Arrange
            var procedure = new Procedure(new Description("Door Oppening"), "Door 01", _MultinivelStep);
            //Act
            procedure.ProcessDuration();
            //Assert
            Assert.AreEqual(0, procedure.Execution.Seconds);
            Assert.AreEqual(4000, procedure.Expected.Seconds);
            Assert.AreEqual(8000, procedure.Limit.Seconds);
        }
        [TestMethod]
        public void OnCompletion_ExecutionTimesIsCorrect()
        {
            //Arrange
            var procedure = new Procedure(new Description("Door Oppening"), "Door 01", _MultinivelStep);
            DateTime firstTime = DateTime.Now;
            DateTime secondTime = DateTime.Now;
            DateTime thirdTime = DateTime.Now;
            DateTime fourthTime = DateTime.Now;
            DateTime fifthTime = DateTime.Now;
            //Act
            procedure.NextInteraction(firstTime);
            procedure.NextInteraction(secondTime);
            procedure.NextInteraction(thirdTime);
            procedure.NextInteraction(fourthTime);
            procedure.NextInteraction(fifthTime);
            //Assert
            Assert.AreEqual(Math.Round(fifthTime.Subtract(firstTime).TotalSeconds), Math.Round(procedure.Execution.Seconds));
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
