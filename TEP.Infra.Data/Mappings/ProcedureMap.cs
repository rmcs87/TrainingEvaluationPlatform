using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TEP.Domain.Entities;
using TEP.Shared.ValueObjects;

namespace TEP.Infra.Data.Mappings
{
    public class ProcedureMap : MapBase<Procedure>
    {
        public override void Configure(EntityTypeBuilder<Procedure> builder)
        {
            base.Configure(builder);
            builder.ToTable("tbl_procedure");
            builder.Property(p => p.Name).IsRequired().HasColumnName("name");
            builder.Property(p => p.Description).IsRequired().HasColumnName("description")
                .HasConversion(e => e.Text, e => new Description(e));           
            builder.Property(p => p.Execution).IsRequired().HasColumnName("execution_time")
                .HasConversion(e => e.Seconds, e => new Duration(e));
            builder.Property(p => p.Expected).IsRequired().HasColumnName("expected_time")
                .HasConversion(e => e.Seconds, e => new Duration(e));
            builder.Property(p => p.Limit).IsRequired().HasColumnName("limit_time")
                .HasConversion(e => e.Seconds, e => new Duration(e));

            //1-1. https://stackoverflow.com/questions/47648487/ef-core-how-to-add-the-relationship-to-shadow-property
            /*builder
                .HasOne(s => s.RootStep)
                .WithOne()
                .HasForeignKey("step_id")
                .OnDelete(DeleteBehavior.Restrict);*/
            //builder.OwnsOne(p => p.RootStep);


            builder.HasOne<Step>(p => p.RootStep)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);
        }

    }
}
