using System;
using System.Collections.Generic;
using System.Text;

namespace TEP.Appication.DTO
{
    public class StepDTO : DTOBase
    {
        public bool Active { get; set; }
        public bool Completed { get; set; }
        public double ExecutionTime { get; set; }       
        public double ExpectedDuration { get; set; }
        public string Name { get; set; }        
        public double LimitDuration { get; set; }
        public string Standard { get; set; }
        public StepType StepType { get; set; }
    }
}
