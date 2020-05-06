using System;
using System.Collections.Generic;
using System.Text;
using TEP.Domain.ValueObjects;

namespace TEP.Domain.Entities
{
    class LeafStep : Step
    {
        public LeafStep(Standard standard, string name, IEnumerable<Step> steps) : base(standard, name)
        {
            Steps = steps;
        }

        public IEnumerable<Step> Steps { get; private set; }
    }
}
