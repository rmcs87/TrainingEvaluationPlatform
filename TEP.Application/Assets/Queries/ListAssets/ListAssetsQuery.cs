using MediatR;
using System.Collections.Generic;
using TEP.Application.Assets.Queries.GetAsset;

namespace TEP.Application.Assets.Queries.ListAssets
{
    public class ListAssetsQuery : IRequest <IEnumerable<AssetDTO>>
    {
    }
}
