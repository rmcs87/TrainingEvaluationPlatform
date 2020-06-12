using System;
using System.Collections.Generic;
using System.Text;
using TEP.Domain.Entities.Assets;
using TEP.Domain.Interfaces.Repositories;
using TEP.Infra.Data.Contexto;

namespace TEP.Infra.Data.Repositories
{
    public class SimpleAssetRepository : RepositoryBase<SimpleAsset>, ISimpleAssetRepository
    {
        public SimpleAssetRepository(Context context) : base(context)
        {
        }
    }
}
