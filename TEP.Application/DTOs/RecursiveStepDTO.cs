using System;
using System.Collections.Generic;
using System.Text;

namespace TEP.Appication.DTO
{
    public class RecursiveStepDTO : StepDTO
    {
        public IEnumerable<StepDTO> StepDTOs { get; set; }
    }
}
