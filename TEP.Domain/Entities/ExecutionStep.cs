using System;
using System.Collections.Generic;
using System.Text;
using TEP.Domain.ValueObjects;
using TEP.Shared.ValueObjects;

namespace TEP.Domain.Entities
{
    class ExecutionStep : LeafStep
    {
        /// <summary>
        /// Creates a Step executed during a Trainning Session.
        /// </summary>
        /// <param name="standard">The classification for a step, which implies how it will be hadled by the trainning APP.</param>
        /// <param name="name">The name fo this Step.</param>
        /// <param name="interactions">The Interactions to be Performed in this Step.</param>
        /// <param name="executionTime">Execution Time spent to execute this step.</param>
        public ExecutionStep(Standard standard, string name, Interaction interaction, Duration executionTime, Step nextStep) : base(standard, name, interaction, nextStep)
        {
            ExecutionTime = executionTime;
        }
        //Gets the Execution Time spent to execute this step.
        public Duration ExecutionTime { get; private set; }
    }
}

