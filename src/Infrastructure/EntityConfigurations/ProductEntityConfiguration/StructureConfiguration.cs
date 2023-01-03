﻿using TechOnIt.Domain.Common;
using TechOnIt.Domain.Entities.Product.StructureAggregate;
using TechOnIt.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TechOnIt.Infrastructure.Common.Consts;

namespace TechOnIt.Infrastructure.EntityConfigurations.ProductEntityConfiguration;

public class StructureConfiguration : IEntityTypeConfiguration<Structure>
{
    public void Configure(EntityTypeBuilder<Structure> builder)
    {
        builder.HasKey(a => a.Id);

        builder.OwnsOne(a => a.ApiKey); // define value object concurrency

        builder.Property(a => a.Type)
            .HasConversion(x => x.Value, x => Enumeration.FromValue<StuctureType>(x));

        #region column types
        builder.Property(a => a.Description).HasColumnType(DataTypes.nvarchar150);
        builder.Property(a => a.Name).HasColumnType(DataTypes.nvarchar50);
        builder.Property(a => a.IsActive).HasColumnType(DataTypes.boolean);
        #endregion

    }
}
