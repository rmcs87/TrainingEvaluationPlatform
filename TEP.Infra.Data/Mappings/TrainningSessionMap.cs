using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TEP.Domain.Entities;
using TEP.Shared.ValueObjects;

namespace TEP.Infra.Data.Mappings
{
    public class TrainningSessionMap : MapBase<TrainningSession>
    {
        public override void Configure(EntityTypeBuilder<TrainningSession> builder)
        {
            base.Configure(builder);
            builder.ToTable("tbl_trainning_session");
            builder.Property(p => p.Date).IsRequired().HasColumnName("execution_date");
            /*
            builder.OwnsOne(p => p.Operator);
            builder.OwnsOne(p => p.Supervisor);
            builder.OwnsOne(p => p.ExecutedProcedure);
            builder.OwnsOne(p => p.TargetProcedure);
            builder.OwnsOne(p => p.Performance);
            */
            builder.HasOne<Operator>(t => t.Operator)
                .WithMany()
                //.HasForeignKey(c => c.Operator)
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne<Supervisor>(t => t.Supervisor)
                .WithMany()
                //.HasForeignKey(c => c.Supervisor)
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne<Procedure>(t => t.ExecutedProcedure)
                .WithMany()
                //.HasForeignKey(c => c.ExecutedProcedure)
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne<Procedure>(t => t.TargetProcedure)
                .WithMany()
                //.HasForeignKey(c => c.TargetProcedure)
                .OnDelete(DeleteBehavior.Restrict);
            builder.OwnsOne(t => t.Performance,
                    p =>
                    {
                        p.Property(p => p.Score).IsRequired().HasColumnName("score");
                        p.Property(p => p.TimeExecution).IsRequired().HasColumnName("time_execution")
                            .HasConversion(t => t.Seconds, e => new Duration(e));
                    }                
                );
        }
    }
}
