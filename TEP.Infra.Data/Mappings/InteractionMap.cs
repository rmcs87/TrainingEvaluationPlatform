﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using TEP.Domain.Entities;
using TEP.Domain.ValueObjects;
using System.Linq;
using TEP.Shared.ValueObjects;
using TEP.Domain.Entities.Assets;

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
            //builder.Property(p => p.Source).IsRequired(false).HasColumnName("source_asset");
            // builder.Property(p => p.Target).IsRequired(false).HasColumnName("target_asset");
            //builder.Ignore(p => p.Source);
            //builder.Ignore(p => p.Target);

            builder.HasOne<SimpleAsset>( p => p.Source)
                .WithMany()
                //.HasForeignKey(c => c.Source)
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne<SimpleAsset>(p => p.Target)
                .WithMany()
                //.HasForeignKey(c => c.Target)
                .OnDelete(DeleteBehavior.Restrict);

            //Problem Known: should not be a enum, and deal with many-to-many . 
            // Possible solution: ownsMany: https://docs.microsoft.com/en-us/ef/core/modeling/owned-entities
            builder.Ignore(i => i.Categories);
            /*builder.Property(p => p.Categories)
                    .IsRequired()
                    .HasColumnName("categories")
                     .HasConversion(e => e.Cast<Category>().FirstOrDefault().ToString(), // to converter
                        e => new List<Category> { (Category)Enum.Parse(typeof(Category), e) });// from converter;   */         
        }
    }
}
