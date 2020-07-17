using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using TEP.Appication.Categories;
using TEP.Application.Common.Mappings;
using TEP.Domain.Entities;
using TEP.Domain.Entities.ManyToMany;

namespace TEP.Application.Assets.Commands.CreateAsset
{
    public class CreateAssetCommand : IRequest<int>, IMapTo<Asset>
    {
        public CreateAssetCommand()
        {
            CategoriesIds = new List<int>();
        }
        public string FileURI { get; set; }
        public string Name { get; set; }
        public IFormFile Image { get; set; }
        [BindProperty(BinderType = typeof(ListCategoryModelBinder))]
        public List<int> CategoriesIds { get; set; }

        public void MappingTo(Profile profile)
        {
            profile.
                CreateMap<CreateAssetCommand, Asset>()                    
                    .ForMember(a => a.AssetCategories, opt => opt.Ignore())                    
                    .ForMember(a => a.Id, opt => opt.Ignore())
                    .ForMember(a => a.IconPath, opt => opt.Ignore())
                    .ForMember(a => a.CreatedBy, opt => opt.Ignore())
                    .ForMember(a => a.Created, opt => opt.Ignore())
                    .ForMember(a => a.LastModifiedBy, opt => opt.Ignore())
                    .ForMember(a => a.LastModified, opt => opt.Ignore());
        }
    }
}
