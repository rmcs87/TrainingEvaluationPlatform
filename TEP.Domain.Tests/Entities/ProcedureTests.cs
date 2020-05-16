using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace TEP.Domain.Tests.Entities
{
    [TestClass]
    public class ProcedureTests
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
        public void OnRequiredAssetsReturnsAllAssetsUsedInSubstepsOfThisProcedure()
        {

        }
    }
}
