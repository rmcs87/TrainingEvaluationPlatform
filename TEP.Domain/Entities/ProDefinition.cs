using System;
using System.Collections.Generic;
using System.Text;
using TEP.Shared.ValueObjects;

namespace TEP.Domain.Entities
{
    class ProDefinition : Procedure
    {
        public ProDefinition(string name, Description description, IEnumerable<Step> steps)  : base(name, description)
        {
            Steps = steps;
        }
        public IEnumerable<Step> Steps { get; private set; }
    }
}
