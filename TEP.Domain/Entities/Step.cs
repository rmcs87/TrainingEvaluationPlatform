using System;
using System.Collections.Generic;
using System.Text;
using TEP.Domain.ValueObjects;

namespace TEP.Domain.Entities
{
    class Step : EntityBase
    {
        public Step(Standard standard, string name)
        {
            Standard = standard;
            Name = name;
        }

        public Standard Standard { get; private set; }
        public string Name { get; private set; }
    }
}
