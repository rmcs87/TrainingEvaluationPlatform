using System;
using System.Collections.Generic;
using System.Text;

namespace TEP.Appication.DTO
{
    public class ProcedureDTO : DTOBase
    {
        public string Description { get; set; }
        public string Name { get; set; }
        public StepDTO RootStep { get; set; }
        public bool Completed { get; set; }
        public double Expected { get; set; }
        public double Limit { get; set; }
        public double Execution { get; set; }
    }
}
