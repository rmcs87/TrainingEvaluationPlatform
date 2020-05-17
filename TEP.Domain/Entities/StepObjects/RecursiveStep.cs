using System;
using System.Collections.Generic;
using System.Text;
using TEP.Domain.ValueObjects;
using TEP.Shared.ValueObjects;

namespace TEP.Domain.Entities.StepEntities
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
        private List<Step> SubSteps { get;  set; }
        /// <summary>
        /// Gets the Current subStep being executed in this step.
        /// </summary>
        public Step CurrentSubStep { get; private set; }

        //Mehods
        /// <summary>
        /// Calculates ExpectedDuration and LimitDuration for this step, considering all subSteps to be performed during the step.
        /// </summary>
        public override void UpdateDuration()
        {
            _expectedDuration = new Duration(0);
            _limitDuration = new Duration(0);
            ExecutionTime = new Duration(0); ;

            foreach (var step in SubSteps)
            {
                step.UpdateDuration();

                ExpectedDuration.Increment(step.ExpectedDuration);
                LimitDuration.Increment(step.LimitDuration);
                
            }
        }
        /// <summary>
        /// Adds a subStep to this Step. The sequence they are added, correspondes to the execution order.
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
            var leafStep = SubSteps[_currentSubStepIndex].AdvanceStep(now);

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
                        CurrentSubStep = null;
                        return null;
                    }
                    else
                    {                        
                        _currentSubStepIndex++;
                        CurrentSubStep = SubSteps[_currentSubStepIndex];
                        leafStep = CurrentSubStep.AdvanceStep(now);
                    }
                }
            }
            else
            {
                throw new InvalidOperationException(message: "This step has already been completed. Can't perform it again.");
            }
            return leafStep;
        }
        /// <summary>
        /// Gets all substeps of this step, if any.
        /// </summary>
        /// <returns>A set of SubSteps.</returns>
        public override List<Step> GetSubSteps()
        {
            return SubSteps;
        }
    }
}
