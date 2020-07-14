using TEP.Domain.Common;

namespace TEP.Domain.Entities
{
    public class Asset : AuditableEntity
    {
        public Asset()
        {
        }
        public Asset(string fileURI, string name, string iconPath)
        {
            FileURI = fileURI;
            Name = name;
            IconPath = iconPath;
        }
        public int Id { get; private set; }
        public string FileURI { get; private set; }
        public string Name { get; private set; }
        public string IconPath { get; private set; }

        public void ChangeName(string newName)
        {
            Name = newName;
        }

        public void UpdateFileURI(string uri)
        {
            FileURI = uri;
        }

        public void UpdateIcon(string iconPath)
        {
            IconPath = iconPath;
        }
    }
}
