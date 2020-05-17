using System;
using System.Collections.Generic;
using System.Text;
using TEP.Domain.ValueObjects;
using TEP.Shared.ValueObjects;

namespace TEP.Domain.Entities.StepEntities
{
    /// <summary>
    /// A Step is part of procedure that may be contitued of several sequential or parallel substeps.
    /// </summary>
    public abstract class Step : EntityBase
    {
        //Constructors
        /// <summary>
        /// A Step is part of procedure that may be constitued of several substeps.
        /// </summary>
        /// <param name="standard">The classification for a step, which implies how it will be hadled by the trainning APP.</param>
        /// <param name="name">The name fo this Step.</param>        
        public Step(Standard standard, string name)
        {
            Standard = standard;
            Name = name;
            ExecutionTime = new Duration(0);
        }

        //Private Variables
        protected Duration _expectedDuration;
        protected Duration _limitDuration;

        //Properties
        /// <summary>
        /// Gets if this Step is being executed
        /// </summary>
        public bool Active { get; protected set; }
        /// <summary>
        /// Gets if this step was completed or not.
        /// </summary>
        public bool Completed { get; protected set; }
        /// <summary>
        /// Gets the time spent to complete this step.
        /// </summary>
        public Duration ExecutionTime{ get; protected set; }
        /// <summary>
        /// Gets the expected duration for this step, including sub steps.
        /// </summary>        
        public Duration ExpectedDuration { get => _expectedDuration ?? ThrowInvalidOperation(); }
        /// <summary>
        /// Gets this name of this Step.
        /// </summary>
        public string Name { get; private set; }
        /// <summary>
        /// Gets the maximum duration for this step, including sub steps. If durations was not calculated, it will call ProcessDuration and return it.
        /// </summary>        
        public Duration LimitDuration { get => _limitDuration ?? ThrowInvalidOperation(); }
        /// <summary>
        /// Gets the classification for this Step.
        /// </summary>
        public Standard Standard { get; private set; }

        //Methods
        /// <summary>
        /// Calculates: Expected, Limit.
        /// </summary>
        public abstract void UpdateDuration();
        /// <summary>
        /// Marks the current Step as completed, and starts the next.
        /// </summary>
        /// <param name="now">The current DateTime when the Step transition occurs. </param>
        /// <returns>The current Active LeafStep, which contains the current Interaction.</returns>
        public abstract LeafStep AdvanceStep(DateTime now);
        /// <summary>
        /// Gets all substeps of this step, if any.
        /// </summary>
        /// <returns>A set of SubSteps.</returns>
        public abstract List<Step> GetSubSteps();
        /// <summary>
        /// Throws an InvalidOperationException.
        /// </summary>
        private Duration ThrowInvalidOperation()
        {
            throw new InvalidOperationException(message: $"{nameof(UpdateDuration)} must be executed before getting Time.");
        }
    }
}
