using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using TEP.Application.Common.Mappings;
using TEP.Domain.Entities;

namespace TEP.Application.Assets.Commands.CreateAsset
{
    public class CreateAssetCommand : IRequest<int>, IMapTo<Asset>
    {
        public string FileURI { get; set; }
        public string Name { get; set; }
        public IFormFile Image { get; set; }

        public void Mapping(Profile profile)
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
