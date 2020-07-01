using MediatR;
using TEP.Appication.DTO;

namespace TEP.Application.Assets.Queries.GetAsset
{
    public class GetAssetQuery : IRequest<AssetDTO>
    {
        public int Id { get; set; }
    }
}
