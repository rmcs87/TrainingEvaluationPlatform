﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TEP.Domain.Entities;

namespace TEP.Infra.Data.Mappings
{
    public class TrainningSessionMap : MapBase<TrainningSession>
    {
        public override void Configure(EntityTypeBuilder<TrainningSession> builder)
        {
            base.Configure(builder);
            builder.ToTable("tbl_trainning_session");
            builder.Property(p => p.Date).IsRequired().HasColumnName("execution_date");

            builder.OwnsOne(p => p.Operator);
            builder.OwnsOne(p => p.Supervisor);
            builder.OwnsOne(p => p.ExecutedProcedure);
            builder.OwnsOne(p => p.TargetProcedure);
            builder.OwnsOne(p => p.Performance);
        }
    }
}
