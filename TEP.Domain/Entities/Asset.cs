using TEP.Domain.Common;

namespace TEP.Domain.Entities
{
    public class Asset : AuditableEntity
    {
        public Asset()
        {
        }

        public Asset(string filePath, string name, string imgPath)
        {
            FilePath = filePath;
            Name = name;
            ImgPath = imgPath;
        }

        public int Id { get; private set; }

        public string FilePath { get; set; }

        public string Name { get; set; }

        public string ImgPath { get; set; }
    }
}
