using MediatR;

namespace TEP.Application.Assets.Queries.GetAssetImg
{
    public class GetAssetImgQuery : IRequest<AssetImgDTO>
    {
        public string ImgName { get; set; }
    }
}
