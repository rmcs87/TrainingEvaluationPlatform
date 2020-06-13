using System;
using System.Collections.Generic;
using TEP.Shared.ValueObjects;
using System.Linq;
using TEP.Domain.Entities;

namespace TEP.Domain.Entities
{
    public class Procedure : EntityBase
    {
        //Constructor
        private Procedure()
        {

        }

        /// <summary>
        /// Constructor for a Procedure, which represents the activity to be performed
        /// in a trainning session.
        /// </summary>
        /// <param name="description">Describes the Procedure.</param>
        /// <param name="name">Names this Procedure.</param>
        /// <param name="rootStep">The rootStep contais all subSteps to perform this procedure.</param>
        public Procedure(Description description, string name, Step rootStep)
        {
            Description = description;
            Name = name;
            RootStep = rootStep;
        }

        //Private Variables
        private Interaction _currentInteraction;

        //Properties
        /// <summary>
        /// Gets this procedure's description.
        /// </summary>
        public Description Description { get; private set; }
        /// <summary>
        /// Gets this procedure's name.
        /// </summary>
        public string Name { get; private set; }
        /// <summary>
        /// Gets this procedure's rootStep.
        /// </summary>
        public Step RootStep { get; private set; }
        /// <summary>
        /// Gets if this procedure is complete.
        /// </summary>
        public bool Completed { get => RootStep.Completed; }
        /// <summary>
        /// Gets Expected Time for this Procedure.
        /// </summary>
        public Duration Expected { get => RootStep.ExpectedDuration;}
        /// <summary>
        /// Gets Limit Time for this Procedure.
        /// </summary>
        public Duration Limit { get => RootStep.LimitDuration; }
        /// <summary>
        /// Gets Execution Time for this Procedure, after completion only.
        /// </summary>
        public Duration Execution { get => RootStep.ExecutionTime; }

        //Methods     
        public Interaction GetCurrentInteraction()
        {
            if(_currentInteraction == null && !Completed)
                throw new InvalidOperationException(message: $"This Procedure has not Been started. Call {nameof(NextInteraction)} to start.");

            return _currentInteraction;
        }
        /// <summary>
        /// Advances the execution of this Procedure to the next interaction. Return Null if there is no more Interactions.
        /// </summary>
        /// <param name="now">The time when the transition form one interaction to the other occurs.</param>
        /// <returns>The Current Interaction after advancing. Returns null if there is no more interactions.</returns>
        public Interaction NextInteraction(DateTime now)
        {
            if(Completed)
                throw new InvalidOperationException(message: "This Procedure has already been completed. Can't perform it again.");

            var nextLeafStep = RootStep.AdvanceStep(now);

            //If null, sets null, else, sets interaction;
            _currentInteraction = nextLeafStep?.Interaction;
             
            return _currentInteraction;
        }
        /// <summary>
        /// Updates values of: expected, limit and execution times. Execution Time is updated only on completion.
        /// </summary>
        public void ProcessDuration()
        {
            RootStep.UpdateDuration();
        }
        /// <summary>
        /// Recover all Assets necessary to perform this procedure, removing duplicates.
        /// </summary>
        /// <returns>A set of All required assets to perform this procedure.</returns>
        public List<Asset> RequiredAssets()
        {
            List<Asset> assets = ExtractAssetFromStep(RootStep);
            //Removes nulls
            assets.RemoveAll(item => item == null);
            //Removes Duplicates
            return new HashSet<Asset>(assets).ToList();
        }
        /// <summary>
        /// Interates through all Steps, reaching all leafs and finding the assets in its interactions.
        /// </summary>
        /// <param name="rootStep"></param>
        /// <returns></returns>
        private List<Asset> ExtractAssetFromStep(Step rootStep)
        {
            var leaf = rootStep as LeafStep;
            var list = new List<Asset>();

            if (leaf != null)
            {                
                list.Add(leaf.Interaction.Source);
                list.Add(leaf.Interaction.Target);
            }
            else { 
                foreach (var s in rootStep.GetSubSteps())
                {
                    list.AddRange(ExtractAssetFromStep(s));
                }
            }
            return list;
        }

    }
}
