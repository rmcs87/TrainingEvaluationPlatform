using System;
using System.Collections.Generic;
using System.Text;
using TEP.Domain.ValueObjects;

namespace TEP.Domain.Entities
{
    class LeafStep : Step
    {
        public LeafStep(Standard standard, string name, IEnumerable<Interaction> interactions) : base(standard, name)
        {
            Interactions = interactions;
        }

        public IEnumerable<Interaction> Interactions { get; private set; }
    }
}
