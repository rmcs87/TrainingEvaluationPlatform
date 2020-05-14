using System;
using System.Collections.Generic;
using System.Text;
using TEP.Domain.ValueObjects;
using TEP.Shared.ValueObjects;

namespace TEP.Domain.Entities.Step
{
    /// <summary>
    /// A Step specialization that contains Interactions only, no substeps are alowed.
    /// </summary>
    public class LeafStep : Step
    {
        //Constructors
        /// <summary>
        /// Creates a leafStep which contais an interaction to be performed.
        /// </summary>
        /// <param name="standard">The classification for a step, which implies how it will be hadled by the trainning APP.</param>
        /// <param name="name">The name fo this Step.</param>
        /// <param name="interaction">The Interactions to be Performed in this Step.</param>
        public LeafStep(Standard standard, string name, Interaction interaction ) : base(standard, name)
        {
            Interaction = interaction;            
        }

        //Private Variables
        private DateTime _startingTime;

        //Properties
        /// <summary>
        /// Gets the interaction performed in this Step.
        /// </summary>
        public Interaction Interaction { get; private set; }

        //Methods
        /// <summary>
        /// Calculates: Expected, Limit.
        /// </summary>     
        public override void CalculateDuration()
        {
            _expectedDuration = new Duration(Interaction.EstimatedTime.Milis);
            _limitDuration = new Duration(Interaction.TimeLimit.Milis);
        }
        /// <summary>
        /// Marks the current Step as completed, and starts the next.
        /// </summary>
        /// <param name="now">The current DateTime when the Step transition occurs.</param>
        /// <returns>The current Active LeafStep, which contains the current Interaction.</returns>
        public override LeafStep AdvanceStep(DateTime now)
        {
            if (!Active && !Completed)
            {
                Active = true;
                _startingTime = now;
            }
            else if(Active && !Completed)
            {
                Active = false;
                Completed = true;
                ExecutionTime.Milis = now.Millisecond - _startingTime.Millisecond;
            }
            else
            {
                throw new InvalidOperationException(message: "This step has already been completed. Can't perform it again.");
            }
            
            return null;
        }
    }
}
