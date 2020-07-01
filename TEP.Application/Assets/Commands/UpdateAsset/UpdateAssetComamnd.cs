using MediatR;
using Microsoft.AspNetCore.Http;

namespace TEP.Application.Assets.Commands.UpdateAsset
{
    public class UpdateAssetComamnd : IRequest
    {
        public int Id { get; set; }
        public string FilePath { get; set; }
        public string Name { get; set; }
        public IFormFile Image { get; set; }
    }
}
