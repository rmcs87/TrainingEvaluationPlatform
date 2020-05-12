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
        public LeafStep(Standard standard, string name, Interaction interaction) : base(standard, name)
        {
            Interaction = interaction;
        }
        /// <summary>
        /// Gets all interactions to be performed in this Step.
        /// </summary>
        public Interaction Interaction { get; private set; }
        /// <summary>
        /// Calculates all times for this step, consifering all Interaction to be performed druing the step.
        /// </summary>
        public override void ProcessDuration()
        {            
            ExpectedDuration.Increment(Interaction.EstimatedTime.Milis);
            LimitDuration.Increment(Interaction.TimeLimit);

            Processed = true;
        }
    }
}
