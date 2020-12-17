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

        public Interaction(Act act, Description description, Duration estimatedTime, Duration timeLimit)
            :this()
        {
            Act = act;
            Description = description;
            EstimatedTime = estimatedTime;
            TimeLimit = timeLimit;
        }

        public Interaction(Act act, Description description, Duration estimatedTime, Duration timeLimit, Asset target)
            :this(act,description,estimatedTime,timeLimit)
        {
            Target = target;
        }
        public Interaction(Act act, Description description, Duration estimatedTime, Duration timeLimit, Asset target, Asset source)
            : this( act, description, estimatedTime, timeLimit, target)
        {
            Source = source;
        }

        public int Id { get; private set; }
        public Act Act { get; private set; }
        public Description Description { get; private set; }
        public Duration EstimatedTime { get; private set; }
        public Duration TimeLimit { get; private set; }
        public Asset Target { get; set; }
        public Asset Source { get; set; }
    }
}
