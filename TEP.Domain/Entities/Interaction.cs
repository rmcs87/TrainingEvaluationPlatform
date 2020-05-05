using System;
using System.Collections.Generic;
using System.Text;
using TEP.Domain.ValueObjects;

namespace TEP.Domain.Entities
{
    class Interaction : EntityBase
    {
        public Category[] Category { get; private set; }
        public Act Act { get; private set; }
        public string Description { get; private set; }
        public float EstimatedTIme { get; private set; }
        public float TimeLimit { get; private set; }
    }
}
