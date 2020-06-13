using System.Collections.Generic;
using TEP.Shared.ValueObjects;
using TEP.Domain.ValueObjects;

namespace TEP.Domain.Entities
{
    /// <summary>
    /// Class that represents an Interaction made by the Operator in a procedure. 
    /// </summary>
    public class Interaction : EntityBase
    {
        //Constructors
        private Interaction()
        {
        }

        /// <summary>
        /// A constructor to create an Interaction with have a source and a target only. (such as inserting a key[source] in a keyhole[target])
        /// </summary>
        /// <param name="category">The categories in which this Interaction is classified.</param>
        /// <param name="act">The act made by the Operator to perform the inteeraction.</param>
        /// <param name="description">A text describing this Interaction, to be used in assisted trainning and manuals.</param>
        /// <param name="estimatedTime">The expected time to perform this interaction.</param>
        /// <param name="timeLimit">The maximum acceptable time to perform the Interaction</param>
        /// <param name="target">The Asset which will receive the interaction</param>
        /// <param name="source">The Asset which will be the source of the interaction</param>
        public Interaction(IEnumerable<Category> category, Act act, Description description, Duration estimatedTime, Duration timeLimit, Asset target, Asset source)
        {
            Categories = category;
            Act = act;
            Description = description;
            EstimatedTime = estimatedTime;
            TimeLimit = timeLimit;
            Target = target;
            Source = source;
        }
        /// <summary>
        /// A constructor to create an objectless Interaction. (such as waiting for a period of time)
        /// </summary>
        /// <param name="category">The categories in which this Interaction is classified.</param>
        /// <param name="act">The act made by the Operator to perform the inteeraction.</param>
        /// <param name="description">A text describing this Interaction, to be used in assisted trainning and manuals.</param>
        /// <param name="estimatedTime">The expected time to perform this interaction.</param>
        /// <param name="timeLimit">The maximum acceptable time to perform the Interaction</param>
        public Interaction(IEnumerable<Category> category, Act act, Description description, Duration estimatedTime, Duration timeLimit)
        {
            Categories = category;
            Act = act;
            Description = description;
            EstimatedTime = estimatedTime;
            TimeLimit = timeLimit;
        }
        /// <summary>
        /// A constructor to create an Interaction with only a target only. (such as waiting pressing a button)
        /// </summary>
        /// <param name="category">The categories in which this Interaction is classified.</param>
        /// <param name="act">The act made by the Operator to perform the inteeraction.</param>
        /// <param name="description">A text describing this Interaction, to be used in assisted trainning and manuals.</param>
        /// <param name="estimatedTime">The expected time to perform this interaction.</param>
        /// <param name="timeLimit">The maximum acceptable time to perform the Interaction</param>
        /// <param name="target">The Asset which will receive the interaction</param>
        public Interaction(IEnumerable<Category> category, Act act, Description description, Duration estimatedTime, Duration timeLimit, Asset target)
        {
            Categories = category;
            Act = act;
            Description = description;
            EstimatedTime = estimatedTime;
            TimeLimit = timeLimit;
            Target = (Asset) target;
        }
        

        //Properties
        /// <summary>
        /// Gets the Categories related to this Interaction.
        /// </summary>
        public IEnumerable<Category> Categories { get; private set; }
        /// <summary>
        /// Gets the Act related to this Interaction.
        /// </summary>
        public Act Act { get; private set; }
        /// <summary>
        /// Gets the Description of this Interaction.
        /// </summary>
        public Description Description { get; private set; }
        /// <summary>
        /// Gets the estimated time to perform this Interaction.
        /// </summary>
        public Duration EstimatedTime { get; private set; }
        /// <summary>
        /// Gets the maximum time to perform this interaction.
        /// </summary>
        public Duration TimeLimit { get; private set; }
        /// <summary>
        /// Gets the Target Asset for this Interaction.
        /// </summary>
        public Asset Target { get; set; }
        /// <summary>
        /// Gets the Source Asset for this Interaction.
        /// </summary>
        public Asset Source { get; set; }
        
    }
}
