using System.Collections.Generic;
using System.Linq;
using TEP.Domain.Common;
using TEP.Domain.Entities.ManyToMany;

namespace TEP.Domain.Entities
{
    public class Asset : AuditableEntity
    {
        public Asset()
        {
            AssetCategories = new List<AssetCategory>();
        }
        public Asset(string fileURI, string name, string iconPath) : this()
        {
            FileURI = fileURI;
            Name = name;
            IconPath = iconPath;
        }
        public int Id { get; private set; }
        public string FileURI { get; private set; }
        public string Name { get; private set; }
        public string IconPath { get; private set; }
        public IEnumerable<AssetCategory> AssetCategories { get; set; }

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
