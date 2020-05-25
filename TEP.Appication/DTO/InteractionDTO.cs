using System;
using System.Collections.Generic;
using System.Text;

namespace TEP.Appication.DTO
{
    public class InteractionDTO : DTOBase
    {
        public IList<String> Category { get; set; }
        public string Act { get; set; }
        public string Description { get; set; }
        public double EstimatedTime { get; set; }
        public double TimeLimit { get; set; }
        public string Target { get; set; }
        public string Source { get; set; }
    }
}
