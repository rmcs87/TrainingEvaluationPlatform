using TEP.Domain.Common;

namespace TEP.Domain.Entities
{
    public class Asset : AuditableEntity
    {
        private Asset()
        {

        }

        public Asset(string filePath, string name, string imgPath)
        {
            FilePath = filePath;
            Name = name;
            ImgPath = imgPath;
        }

        public int Id { get; private set; }

        public string FilePath { get; private set; }

        public string Name { get; private set; }

        public string ImgPath { get; private set; }
    }
}
