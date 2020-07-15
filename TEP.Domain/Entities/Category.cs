using System.Collections.Generic;
using TEP.Domain.Common;
using TEP.Domain.Entities.ManyToMany;

namespace TEP.Domain.Entities
{
    public class Category : AuditableEntity
    {
        public Category()
        {
            AssetCategories = new List<AssetCategory>();
        }
        public Category(string code) : this()
        {
            Code = code;
        }
        public Category(string name, string code) : this(code)
        {
            Name = name;
        }
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Code { get; private set; }
        public ICollection<AssetCategory> AssetCategories { get; private set; }

        public void UpdateName(string name)
        {
            Name = name;
        }
    }
}
