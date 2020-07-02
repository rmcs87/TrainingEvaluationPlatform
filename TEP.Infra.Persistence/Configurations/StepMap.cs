//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Metadata.Builders;
//using System;
//using System.Collections.Generic;
//using System.Text;
//using TEP.Domain.Entities;

//namespace TEP.Infra.Data.Configurations
//{
//    public class StepMap : MapBase<Step>
//    {
//        public override void Configure(EntityTypeBuilder<Step> builder)
//        {
//            base.Configure(builder);
//            builder
//                .ToTable("tbl_steps")
//                .HasDiscriminator<int>("step_type");
//        }
//    }
//}
