using System;
using System.Collections.Generic;
using System.Text;
using TEP.Shared.ValueObjects;

namespace TEP.Domain.Entities
{
    class ProExecution : Procedure
    {
        public ProExecution(string name, Description description, IEnumerable<LeafStep> steps) : base(name, description)
        {
            LeafSteps = steps;
        }
        public IEnumerable<LeafStep> LeafSteps { get; private set; }
    }
}
