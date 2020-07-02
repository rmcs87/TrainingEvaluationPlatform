using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TEP.Domain.Entities;

namespace TEP.Infra.Persistence.Configurations
{
    public class AssetConfiguration : IEntityTypeConfiguration<Asset>
    {
        public void Configure(EntityTypeBuilder<Asset> builder)
        {
            builder.Property(a => a.Name)
                .HasMaxLength(200)
                .IsRequired();
        }
    }
}
