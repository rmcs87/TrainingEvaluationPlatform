using System;
using System.Collections.Generic;
using TEP.Shared.ValueObjects;
using System.Linq;
using TEP.Domain.Common;
using TEP.Domain.ValueObjects;

namespace TEP.Domain.Entities
{
    public class Procedure : AuditableEntity
    {
        private Procedure()
        {
        }

        public Procedure(Description description, string name, Step rootStep)
        {
            Description = description;
            Name = name;
            RootStep = rootStep;
        }

        private Interaction _currentInteraction;

        public int Id { get; private set; }
        public Description Description { get; private set; }
        public string Name { get; private set; }
        public Step RootStep { get; private set; }
        public bool Completed { get => RootStep.Completed; }

        public Duration Expected { get => RootStep.ExpectedDuration; }
        public Duration Limit { get => RootStep.LimitDuration; }
        public Duration Execution { get => RootStep.ExecutionTime; }

        public Interaction GetCurrentInteraction()
        {
            if (_currentInteraction == null && !Completed)
            {
                throw new InvalidOperationException(message: $"This Procedure has not Been started. Call {nameof(NextInteraction)} to start.");
            }
            return _currentInteraction;
        }

        public Interaction NextInteraction(DateTime now)
        {
            if (Completed)
                throw new InvalidOperationException(message: "This Procedure has already been completed. Can't perform it again.");

            var nextLeafStep = RootStep.AdvanceStep(now);

            //If null, sets null, else, sets interaction;
            _currentInteraction = nextLeafStep?.Interaction;

            return _currentInteraction;
        }

        public void ProcessDuration()
        {
            RootStep.UpdateDuration();
        }

        public List<Asset> RequiredAssets()
        {
            List<Asset> assets = ExtractAssetFromStep(RootStep);
            //Removes nulls
            assets.RemoveAll(item => item == null);
            //Removes Duplicates
            return new HashSet<Asset>(assets).ToList();
        }

        private List<Asset> ExtractAssetFromStep(Step rootStep)
        {
            var leaf = rootStep as LeafStep;
            var list = new List<Asset>();

            if (leaf != null)
            {
                list.Add(leaf.Interaction.Source);
                list.Add(leaf.Interaction.Target);
            }
            else
            {
                foreach (var s in rootStep.GetSubSteps())
                {
                    list.AddRange(ExtractAssetFromStep(s));
                }
            }
            return list;
        }

    }
}
