using AutoMapper;
using TEP.Application.Common.Mappings;
using TEP.Domain.Entities;

namespace TEP.Appication.Categories
{
    public class CategoryDTO : IMapFrom<Category>, IMapTo<Category>
    {
        public string Name { get; set; }
        public string Code { get; set; }

        public void MappingTo(Profile profile)
        {
            profile.
                CreateMap<CategoryDTO, Category>()

                    .ForMember(a => a.AssetCategories, opt=> opt.Ignore())
                    .ForMember(a => a.Id, opt => opt.Ignore())
                    .ForMember(a => a.Created, opt => opt.Ignore())
                    .ForMember(a => a.CreatedBy, opt => opt.Ignore())
                    .ForMember(a => a.LastModifiedBy, opt => opt.Ignore())
                    .ForMember(a => a.LastModified, opt => opt.Ignore());
        }
    }    
}
