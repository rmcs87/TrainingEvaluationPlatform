using System;
using System.Collections.Generic;
using System.Text;

namespace TEP.Appication.DTO
{
    public class AssetDTO : DTOBase
    {
        public string FilePath { get; private set; }
        public string Name { get; private set; }
        public string ImgPath { get; private set; }
    }
}
