using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TEP.Application.Assets.Commands.CreateAsset
{
    
    public class CreateAssetCommand : IRequest<int>
    {
        public string FilePath { get; set; }
        public string Name { get; set; }
        public IFormFile Image { get; set; }
    }
}
