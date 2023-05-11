﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using TechOnIt.Infrastructure.Common.Consts;
using TechOnIt.Domain.Entities.StructureAggregate;

namespace TechOnIt.Infrastructure.Persistence.Configurations.ProductEntityConfiguration;

public class PlaceConfiguration : IEntityTypeConfiguration<Place>
{
    public void Configure(EntityTypeBuilder<Place> builder)
    {
        builder.HasKey(a => a.Id);
        builder.Property(a => a.Name).IsRequired();
        builder.Property(a => a.CreatedAt).IsRequired();

        builder.HasOne(a => a.Structure)
            .WithMany(a => a.Places)
            .HasForeignKey(a => a.StructureId);

        #region column types
        builder.Property(a => a.Name).HasColumnType(DataTypes.nvarchar50);
        builder.Property(a => a.Description).HasColumnType(DataTypes.nvarchar150);
        #endregion
    }
}
