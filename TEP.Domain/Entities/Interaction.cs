using System;
using System.Collections.Generic;
using System.Text;
using TEP.Domain.ValueObjects;
using TEP.Shared.ValueObjects;

namespace TEP.Domain.Entities
{
    class Interaction : EntityBase
    {
        public Interaction(IEnumerable<Category> category, Act act, Description description, Duration estimatedTime, Duration timeLimit)
        {
            Category = category;
            Act = act;
            Description = description;
            EstimatedTime = estimatedTime;
            TimeLimit = timeLimit;
        }

        public IEnumerable<Category> Category { get; private set; }
        public Act Act { get; private set; }
        public Description Description { get; private set; }
        public Duration EstimatedTime { get; private set; }
        public Duration TimeLimit { get; private set; }
    }
}
