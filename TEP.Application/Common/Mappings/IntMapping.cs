using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using TEP.Domain.Entities.ManyToMany;

namespace TEP.Application.Common.Mappings
{
    public class IntMapping : IMapTo<AssetCategory>
    {
        public void MappingTo(Profile profile)
        {
            profile.
                CreateMap<int, AssetCategory>()
                    .ForMember(ac => ac.CategoryId, act => act.MapFrom(i => i))
                    .ForMember(ac => ac.Asset, opt => opt.Ignore())
                    .ForMember(ac => ac.AssetId, opt => opt.Ignore())
                    .ForMember(ac => ac.Category, opt => opt.Ignore());
        }
    }
}
