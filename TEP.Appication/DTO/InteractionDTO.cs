using System;
using System.Collections.Generic;
using System.Text;

namespace TEP.Appication.DTO
{
    public class InteractionDTO : DTOBase
    {
        public IList<CategoryDTO> Categories { get; set; }
        public string Act { get; set; }
        public string Description { get; set; }
        public double EstimatedTime { get; set; }
        public double TimeLimit { get; set; }
        public AssetDTO Target { get; set; }
        public AssetDTO Source { get; set; }
    }
}
