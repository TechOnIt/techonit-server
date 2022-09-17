﻿using iot.Application.Repositories.SQL;
using iot.Application.Repositories.SQL.Roles;
using iot.Application.Repositories.SQL.StructureAggregateRepository;
using iot.Application.Repositories.SQL.Users;
using iot.Infrastructure.Persistence.Context.Identity;

namespace iot.Application.Repositories.UnitOfWorks.Identity;

public interface IUnitOfWorks
{
    Task SaveAsync(CancellationToken stoppingToken = default);
    ISqlRepository<TEntity> SqlRepository<TEntity>() where TEntity : class;
    IIdentityContext _context { get; }

    IStructureRepository StructureRepository { get; }
    IUserRepository UserRepository { get; }
    IRoleRepository RoleRepository { get; }
}