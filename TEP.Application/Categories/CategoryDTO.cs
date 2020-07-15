using TEP.Application.Common.Mappings;
using TEP.Domain.Entities;

namespace TEP.Appication.Categories
{
    public class CategoryDTO : IMapFrom<Category>
    {
        public string Name { get; private set; }
        public string Code { get; private set; }
    }
}
