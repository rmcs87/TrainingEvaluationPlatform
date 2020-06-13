using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TEP.Domain.Entities;
using TEP.Domain.ValueObjects;
using TEP.Shared.ValueObjects;

namespace TEP.Infra.Data.Mappings
{
    public class InteractionMap : MapBase<Interaction>
    {
        public override void Configure(EntityTypeBuilder<Interaction> builder)
        {
            base.Configure(builder);
            builder.ToTable("tbl_interaction");
            builder.Property(p => p.Act).IsRequired().HasColumnName("act");
            builder.Property(p => p.Description).IsRequired().HasColumnName("description")
                .HasConversion(e => e.Text , e => new Description(e));
            builder.Property(p => p.EstimatedTime).IsRequired().HasColumnName("estimated_time")
                .HasConversion(e => e.Seconds, e => new Duration( e ));           
            builder.Property(p => p.TimeLimit).IsRequired().HasColumnName("limit_time")
                .HasConversion(e => e.Seconds, e => new Duration(e));
            builder.HasOne<Asset>( p => p.Source)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne<Asset>(p => p.Target)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);
            builder.OwnsMany<Category>(i => i.Categories);        
        }
    }
}
