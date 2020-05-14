using System;
using System.Collections.Generic;
using System.Text;
using TEP.Domain.ValueObjects;
using TEP.Shared.ValueObjects;

namespace TEP.Domain.Entities.Step
{
    public class RecursiveStep : Step
    {
        //Constructors.
        /// <summary>
        /// Creates a Step constituted of other subSteps: both leaves or recursives.
        /// </summary>
        /// <param name="standard">The classification for a step, which implies how it will be hadled by the trainning APP.</param>
        /// <param name="name">The name fo this Step.</param>
        /// <param name="subSteps">The SubSteps to be Performed in this Step.</param>
        public RecursiveStep(Standard standard, string name, List<Step> subSteps) : base(standard, name)
        {
            SubSteps = subSteps;
        }

        //Private Variables
        private int _currentSubStepIndex = 0;
        
        //Properties
        /// <summary>
        /// Gets all SubSteps to be performed in this Step.
        /// </summary>
        public List<Step> SubSteps { get; private set; }
        /// <summary>
        /// Gets the Current subStep being executed in this step.
        /// </summary>
        public Step CurrentStep { get => SubSteps[_currentSubStepIndex]; }

        //Mehods
        /// <summary>
        /// Calculates ExpectedDuration and LimitDuration for this step, considering all subSteps to be performed during the step.
        /// </summary>
        public override void CalculateDuration()
        {
            _expectedDuration = new Duration(0.0f);
            _limitDuration = new Duration(0.0f);

            foreach (var step in SubSteps)
            {
                step.CalculateDuration();

                ExpectedDuration.Increment(step.ExpectedDuration);
                LimitDuration.Increment(step.LimitDuration);                
            }
        }
        /// <summary>
        /// Adds a subStep to this Step.
        /// </summary>
        /// <param name="step">subStep to be executed in this Step.</param>
        public void AddSubStep(Step step)
        {
            SubSteps.Add(step);
        }
        /// <summary>
        /// Removes a subStep from this Step.
        /// </summary>
        /// <param name="step">subStep to be executed in this Step.</param>
        public void RemoveSubStep(Step step)
        {
            SubSteps.Remove(step);
        }
        /// <summary>
        /// Verifies if the current subStep is the last for this step.
        /// </summary>
        /// <returns></returns>
        private bool IsLastSubStep()
        {
            return !(_currentSubStepIndex < SubSteps.Count - 1);
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
            }
            else if (Active && !Completed)
            {
                if (SubSteps[_currentSubStepIndex].Completed)
                {
                    if (IsLastSubStep())
                    {
                        Active = false;
                        Completed = true;
                        return null;
                    }
                    else
                    {                        
                        _currentSubStepIndex++;
                    }
                }
            }
            else
            {
                throw new InvalidOperationException(message: "This step has already been completed. Can't perform it again.");
            }
            return SubSteps[_currentSubStepIndex].AdvanceStep(now);
        }        
    }
}
