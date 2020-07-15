using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TEP.Domain.Entities.ManyToMany;

namespace TEP.Infra.Persistence.Configurations
{
    public class AssetCategoryConfiguration : IEntityTypeConfiguration<AssetCategory>
    {
        public void Configure(EntityTypeBuilder<AssetCategory> builder)
        {
            builder
                .HasKey(t => new { t.AssetId, t.CategoryId});

            builder
                .HasOne(ac => ac.Asset)
                .WithMany(a => a.AssetCategories)
                .HasForeignKey(ac => ac.AssetId);

            builder
                .HasOne(ac => ac.Category)
                .WithMany(c => c.AssetCategories)
                .HasForeignKey(ac => ac.AssetId);

        }
    }
}
