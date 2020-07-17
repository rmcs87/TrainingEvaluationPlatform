using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using TEP.Appication.Categories;
using TEP.Application.Common.Mappings;
using TEP.Domain.Entities;

namespace TEP.Application.Assets.Queries.GetAsset
{
    public class AssetDTO : IMapFrom<Asset>
    {
        public int Id { get; set; }
        public string FileURI { get; set; }
        public string Name { get; set; }
        public string IconPath { get; set; }
        public IEnumerable<CategoryDTO> CategoryDTOs { get; set; }

        public void MappingFrom(Profile profile)
        {
            profile.
                CreateMap<Asset, AssetDTO>()
                    .ForMember(dto => dto.CategoryDTOs, 
                        act => act.MapFrom(a => a.AssetCategories.Select(ac => ac.Category).ToList()));
        }
    }   
}
