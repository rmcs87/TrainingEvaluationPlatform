using MediatR;
using Microsoft.AspNetCore.Http;

namespace TEP.Application.Assets.Commands.CreateAsset
{
    public class CreateAssetCommand : IRequest<int>
    {
            public string FilePath { get; set; }
            public string Name { get; set; }
            public IFormFile Image { get; set; }    
    }
}
