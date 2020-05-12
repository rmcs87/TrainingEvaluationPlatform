using System;
using System.Collections.Generic;
using System.Text;
using TEP.Domain.ValueObjects;
using TEP.Shared.ValueObjects;

namespace TEP.Domain.Entities
{
    class ExecutionStep : LeafStep
    {
        public ExecutionStep(Standard standard, string name, IEnumerable<Interaction> interactions) : base(standard, name, interactions)
        {
        }

        public Duration ExecutionTime { get; private set; }
    }
}

