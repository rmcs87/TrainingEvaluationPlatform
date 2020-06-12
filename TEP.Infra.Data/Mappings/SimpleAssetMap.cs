using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TEP.Domain.Entities.Assets;

namespace TEP.Infra.Data.Mappings
{
    public class SimpleAssetMap : MapBase<SimpleAsset>
    {
        public override void Configure(EntityTypeBuilder<SimpleAsset> builder)
        {
            base.Configure(builder);
            builder.ToTable("tbl_simple_asset");
        }
    }
}
