using System;
using System.Collections.Generic;
using System.Text;
using TEP.Domain.ValueObjects;
using TEP.Shared.ValueObjects;

namespace TEP.Domain.Entities.Step
{
    /// <summary>
    /// A Step is part of procedure that may be contitued of several sequential or parallel substeps.
    /// </summary>
    public abstract class Step : EntityBase
    {
        /// <summary>
        /// A Step is part of procedure that may be contitued of several sequential or parallel substeps.
        /// </summary>
        /// <param name="standard">The classification for a step, which implies how it will be hadled by the trainning APP.</param>
        /// <param name="name">The name fo this Step.</param>        
        public Step(Standard standard, string name)
        {
            Standard = standard;
            Name = name;

            _expectedDuration = new Duration(0.0f);
            _limitDuration = new Duration(0.0f);
        }

        private Duration _expectedDuration;
        private Duration _limitDuration;

        /// <summary>
        /// Gets if this Step is being executed
        /// </summary>
        public bool Active { get; protected set; }
        /// <summary>
        /// Gets if this Step was processed to calculate expected and limi times;
        /// </summary>
        public bool Processed { get; protected set; }
        /// <summary>
        /// Gets the classification for this Step.
        /// </summary>
        public Standard Standard { get; private set; }
        /// <summary>
        /// Gets this name of this Step.
        /// </summary>
        public string Name { get; private set; }       
        /// <summary>
        /// Gets the expected duration for this step, including sub steps.
        /// </summary>        
        public Duration ExpectedDuration { get => Processed ? _expectedDuration : ThrowInvalidOperation();}        
        /// <summary>
        /// Gets the maximum duration for this step, including sub steps. If durations was not calculated, it will call ProcessDuration and return it.
        /// </summary>        
        public Duration LimitDuration { get => Processed ? _limitDuration : ThrowInvalidOperation(); }
        /// <summary>
        /// Gets if this step was completed or not.
        /// </summary>
        public bool Completed { get; protected set; }
        /// <summary>
        /// Gets the time spent to complete this step.
        /// </summary>
        public Duration CompletionDuration { get; protected set; }


        /// <summary>
        /// Starts the current step.
        /// </summary>
        public abstract void ProcessDuration();
        /// <summary>
        /// Throws an InvalidOperationException.
        /// </summary>
        private Duration ThrowInvalidOperation()
        {
            throw new InvalidOperationException(message: $"{nameof(ProcessDuration)} must be executed before getting Time.");
        }

    }

}
