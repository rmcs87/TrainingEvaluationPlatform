using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using TEP.Domain.Entities;

namespace TEP.Infra.Data.Mappings
{
    public class WorkerMap : MapBase <Worker>
    {
        public override void Configure(EntityTypeBuilder<Worker> builder)
        {
            base.Configure(builder);
            builder
                .ToTable("tbl_workers")
                .HasDiscriminator<int>("worker_type");
        }
    }
}
