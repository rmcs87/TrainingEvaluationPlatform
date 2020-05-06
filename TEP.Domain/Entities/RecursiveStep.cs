using System;
using System.Collections.Generic;
using System.Text;
using TEP.Domain.ValueObjects;

namespace TEP.Domain.Entities
{
    class RecursiveStep : Step
    {
        public RecursiveStep(Standard standard, string name, IEnumerable<Action> actions) : base(standard, name)
        {
            Actions = actions;
        }

        public IEnumerable<Action> Actions { get; private set; }
    }
}
