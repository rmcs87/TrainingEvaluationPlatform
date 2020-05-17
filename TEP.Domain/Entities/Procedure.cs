using System;
using System.Collections.Generic;
using System.Text;
using TEP.Shared.ValueObjects;
using TEP.Domain.Entities.StepEntities;
using TEP.Shared;
using System.Linq;

namespace TEP.Domain.Entities
{
    public class Procedure : EntityBase
    {
        //Constructor
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
        /// Gets Expected Time for this Procedure.
        /// </summary>
        public Duration Expected { get; private set; }
        /// <summary>
        /// Gets Limit Time for this Procedure.
        /// </summary>
        public Duration Limit { get; private set; }
        /// <summary>
        /// Gets Execution Time for this Procedure.
        /// </summary>
        public Duration Execution { get; private set; }

        //Methods
        /// <summary>
        /// FullFills the Procedure with Steps.
        /// </summary>
        /// <param name="json">JSON object with steps.</param>
        public void FromJson(string json)
        {
            //2do
            //https://docs.microsoft.com/pt-br/dotnet/standard/serialization/system-text-json-how-to
            //https://www.newtonsoft.com/json
            throw new NotImplementedException();
        }
        /// <summary>
        /// Transforms this procedure into a JSON.
        /// </summary>
        /// <returns>Return the JSON object relative to this Procedure.</returns>
        public string ToJson()
        {
            //2do
            //https://docs.microsoft.com/pt-br/dotnet/standard/serialization/system-text-json-how-to
            //https://www.newtonsoft.com/json
            throw new NotImplementedException();
        }
        /// <summary>
        /// Gets the currentInteraction under execution.
        /// </summary>
        /// <returns>The Current Interaction.</returns>
        public Interaction CurrentInteraction()
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Advances the execution of this Procedure to the next interaction.
        /// </summary>
        /// <returns>The Current Interaction after advancing. Returns null if there is no more interactions.</returns>
        public Interaction NextInteraction()
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Updates values of: expected, limit and execution times.
        /// </summary>
        public void UpdateDuration()
        {

        }
        /// <summary>
        /// Recover all Assets necessary to perform this procedure, removing duplicates.
        /// </summary>
        /// <returns>A set of All required assets to perform this procedure.</returns>
        public List<IAsset> RequiredAssets()
        {
            List<IAsset> assets = ExtractIAssetFromStep(RootStep);
            assets.RemoveAll(item => item == null);
            return new HashSet<IAsset>(assets).ToList();
        }
        /// <summary>
        /// Interates through all Steps, reaching all leafs and finding the assets in its interactions.
        /// </summary>
        /// <param name="rootStep"></param>
        /// <returns></returns>
        private List<IAsset> ExtractIAssetFromStep(Step rootStep)
        {
            var leaf = rootStep as LeafStep;
            var list = new List<IAsset>();

            if (leaf != null)
            {                
                list.Add(leaf.Interaction.Source);
                list.Add(leaf.Interaction.Target);
            }
            else { 
                foreach (var s in rootStep.GetSubSteps())
                {
                    list.AddRange(ExtractIAssetFromStep(s));
                }
            }
            return list;
        }

    }
}
