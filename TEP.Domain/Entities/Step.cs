using System;
using System.Collections.Generic;
using System.Text;
using TEP.Domain.ValueObjects;
using TEP.Shared.ValueObjects;

namespace TEP.Domain.Entities
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
        /// <param name="nextStep">the next step of the Procedure. Returns NULL if it is the last step. In case of OUTOFORDER steps an default next Step shall be provided</param>
        public Step(Standard standard, string name, Step nextStep)
        {
            Standard = standard;
            Name = name;
            NextStep = nextStep;

            _expectedDuration = new Duration(0.0f);
            _limitDuration = new Duration(0.0f);
        }

        private Duration _expectedDuration;
        private Duration _limitDuration;

        public bool Processed { get; set; }
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
        /// Gets the next step of the Procedure. Returns NULL if it is the last step. In case of OUTOFORDER steps
        /// an default next Step shall be provided
        /// </summary>
        public Step NextStep { get; private set; }
        /// <summary>
        /// Calculates all times for this step, inlcuding substeps when present.
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
