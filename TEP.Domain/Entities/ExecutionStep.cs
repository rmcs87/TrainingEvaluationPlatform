using System;
using System.Collections.Generic;
using System.Text;
using TEP.Domain.ValueObjects;
using TEP.Shared.ValueObjects;

namespace TEP.Domain.Entities
{
    class ExecutionStep : Step
    {
        public ExecutionStep(Standard standard, string name, IEnumerable<Action> actions) : base(standard, name)
        {
            Actions = actions;
        }

        public IEnumerable<Action> Actions { get; private set; }
        public Duration ExecutionTime { get; private set; }
    }
}
}
