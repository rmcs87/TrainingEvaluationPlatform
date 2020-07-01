using AutoMapper;
using TEP.Application.Common.Mappings;
using TEP.Domain.Entities;

namespace TEP.Appication.DTO
{
    public class AssetDTO : IMapFrom<Asset>
    {
        public int Id { get; set; }
        public string FilePath { get; set; }
        public string Name { get; set; }
        public string ImgPath { get; set; }
        //public IFormFile Image { get; set; }

    }
}
