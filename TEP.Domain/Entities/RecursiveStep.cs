using System;
using System.Collections.Generic;
using System.Text;
using TEP.Domain.ValueObjects;

namespace TEP.Domain.Entities
{
    class RecursiveStep : Step
    {
        /// <summary>
        /// Creates a Step constituted of other Steps: both leaves or recursives.
        /// </summary>
        /// <param name="standard">The classification for a step, which implies how it will be hadled by the trainning APP.</param>
        /// <param name="name">The name fo this Step.</param>
        /// <param name="subSteps">The SubSteps to be Performed in this Step.</param>
        /// <param name="relation">The ytpe of Relation among the subSteps inside this Step.</param>
        public RecursiveStep(Standard standard, string name, IEnumerable<Step> subSteps, Relation relation, Step nextStep) : base(standard, name, nextStep)
        {
            SubSteps = subSteps;
            Relation = Relation;
        }
        /// <summary>
        /// Gets all SubSteps to be performed in this Step.
        /// </summary>
        public IEnumerable<Step> SubSteps { get; private set; }
        /// <summary>
        /// Gets the type of retlation for subStepes inside this Step
        /// </summary>
        public Relation Relation { get; private set; }
        /// <summary>
        /// Calculates all times for this step, consifering all Interaction to be performed druing the step.
        /// </summary>
        public override void ProcessDuration()
        {
            foreach (var step in SubSteps)
            {
                if (!step.Processed)
                {
                    step.ProcessDuration();
                }
                Processed = true;

                ExpectedDuration.Increment(step.ExpectedDuration);
                LimitDuration.Increment(step.LimitDuration);                
            }
        }
    }
}
