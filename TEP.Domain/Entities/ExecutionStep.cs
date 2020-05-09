using System;
using System.Collections.Generic;
using System.Text;
using TEP.Domain.ValueObjects;
using TEP.Shared.ValueObjects;

namespace TEP.Domain.Entities
{
    class ExecutionStep : Step
    {
        public ExecutionStep(Standard standard, string name, IEnumerable<Interaction> interactions) : base(standard, name)
        {
            Interactions = interactions;
        }

        public IEnumerable<Interaction> Interactions { get; private set; }
        public Duration ExecutionTime { get; private set; }
    }
}

