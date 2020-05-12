using System;
using System.Collections.Generic;
using System.Text;
using TEP.Domain.ValueObjects;
using TEP.Shared.ValueObjects;

namespace TEP.Domain.Entities
{
    /// <summary>
    /// A Step specialization that contains Interactions only, no substeps are alowed.
    /// </summary>
    class LeafStep : Step
    {
        /// <summary>
        /// Creates a Step constituted of Interactions.
        /// </summary>
        /// <param name="standard">The classification for a step, which implies how it will be hadled by the trainning APP.</param>
        /// <param name="name">The name fo this Step.</param>
        /// <param name="interactions">The Interactions to be Performed in this Step.</param>
        public LeafStep(Standard standard, string name, IEnumerable<Interaction> interactions) : base(standard, name)
        {
            Interactions = interactions;
        }
        /// <summary>
        /// Gets all interactions to be performed in this Step.
        /// </summary>
        public IEnumerable<Interaction> Interactions { get; private set; }
        /// <summary>
        /// Calculates all times for this step, consifering all Interaction to be performed druing the step.
        /// </summary>
        public override void ProcessDuration()
        {
            foreach (var interaction in Interactions)
            {
                ExpectedDuration.Increment(interaction.EstimatedTime.Milis);
                LimitDuration.Increment(interaction.TimeLimit);
            }
            Processed = true;
        }
    }
}
