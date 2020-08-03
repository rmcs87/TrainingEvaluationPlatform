using System;
using System.Collections.Generic;
using TEP.Domain.ValueObjects;

namespace TEP.Domain.Entities
{
    public class RecursiveStep : Step
    {
        public RecursiveStep(Standard standard, string name, List<Step> subSteps) : base(standard, name)
        {
            SubSteps = subSteps;
        }

        private int _currentSubStepIndex = 0;
        public List<Step> SubSteps { get; private set; }
        public Step CurrentSubStep { get; private set; }

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

        public void AddSubStep(Step step)
        {
            SubSteps.Add(step);
        }

        public void RemoveSubStep(Step step)
        {
            SubSteps.Remove(step);
        }

        private bool IsLastSubStep()
        {
            return !(_currentSubStepIndex < SubSteps.Count - 1);
        }

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
                    ExecutionTime.Increment( SubSteps[_currentSubStepIndex].ExecutionTime );
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

        public override List<Step> GetSubSteps()
        {
            return SubSteps;
        }
    }
}
