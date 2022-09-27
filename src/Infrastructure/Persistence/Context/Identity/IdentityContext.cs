﻿using iot.Domain.Entities.Identity;
using iot.Domain.Entities.Product;
using iot.Domain.Entities.Product.StructureAggregate;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace iot.Infrastructure.Persistence.Context.Identity;

public class IdentityContext : DbContext, IIdentityContext
{
    public IdentityContext(DbContextOptions<IdentityContext> options)
        : base(options) { }

#nullable disable
    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<UserRole> UserRoles { get; set; }
    public DbSet<LoginHistory> LoginHistories { get; set; }
#nullable enable

    #region products
    public DbSet<Structure> Structures {get;set;}
    public DbSet<Place> Places { get; set; }
    public DbSet<Device> Devices { get; set; }
    public DbSet<Sensor> Sensors { get; set; }
    public DbSet<PerformanceReport> PerformanceReports { get; set; }
    #endregion

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}