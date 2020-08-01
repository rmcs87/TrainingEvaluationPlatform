using System.Collections.Generic;
using TEP.Shared.ValueObjects;
using TEP.Domain.ValueObjects;
using TEP.Domain.Common;

namespace TEP.Domain.Entities
{
    public class Interaction : AuditableEntity
    {
        private Interaction()
        {
        }

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

        public Interaction(IEnumerable<Category> category, Act act, Description description, Duration estimatedTime, Duration timeLimit)
        {
            Categories = category;
            Act = act;
            Description = description;
            EstimatedTime = estimatedTime;
            TimeLimit = timeLimit;
        }

        public Interaction(IEnumerable<Category> category, Act act, Description description, Duration estimatedTime, Duration timeLimit, Asset target)
        {
            Categories = category;
            Act = act;
            Description = description;
            EstimatedTime = estimatedTime;
            TimeLimit = timeLimit;
            Target = target;
        }

        public int Id { get; private set; }
        public IEnumerable<Category> Categories { get; private set; }
        public Act Act { get; private set; }
        public Description Description { get; private set; }
        public Duration EstimatedTime { get; private set; }
        public Duration TimeLimit { get; private set; }
        public Asset Target { get; set; }
        public Asset Source { get; set; }
        
    }
}
