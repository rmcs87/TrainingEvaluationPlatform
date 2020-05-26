using System;
using System.Collections.Generic;
using System.Text;
using TEP.Shared.ValueObjects;

namespace TEP.Domain.ValueObjects
{
    public class Performance
    {
        public float Score { get; set; }
        public Duration TimeExecution { get; set; }
    }
}
