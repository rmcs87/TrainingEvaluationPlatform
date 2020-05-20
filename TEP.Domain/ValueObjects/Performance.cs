using System;
using System.Collections.Generic;
using System.Text;
using TEP.Shared.ValueObjects;

namespace TEP.Domain.ValueObjects
{
    public class Performance
    {
        public float Score { get; private set; }
        public Duration TimeExecution { get; private set; }
    }
}
