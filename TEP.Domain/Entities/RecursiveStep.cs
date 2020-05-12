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
        public RecursiveStep(Standard standard, string name, IEnumerable<Step> subSteps) : base(standard, name)
        {
            SubSteps = subSteps;
        }
        /// <summary>
        /// Gets all SubSteps to be performed in this Step.
        /// </summary>
        public IEnumerable<Step> SubSteps { get; private set; }
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
                ExpectedDuration.Increment(step.ExpectedDuration);
                LimitDuration.Increment(step.LimitDuration);
                Processed = true;
            }
        }
    }
}
