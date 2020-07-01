using MediatR;
using System.Collections.Generic;
using TEP.Appication.DTO;

namespace TEP.Application.Assets.Queries.ListAssets
{
    public class ListAssetsQuery : IRequest <IEnumerable<AssetDTO>>
    {
    }
}
