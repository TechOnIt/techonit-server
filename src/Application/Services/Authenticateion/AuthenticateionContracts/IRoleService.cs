﻿using TechOnIt.Application.Common.Enums.IdentityService;
using TechOnIt.Domain.Entities.Identity.UserAggregate;

namespace TechOnIt.Application.Services.Authenticateion.AuthenticateionContracts;

public interface IRoleService
{
    Task<(IdentityCrudStatus Result, string Message)> CreateRoleAsync(Role role,
        CancellationToken cancellationToken = default);
    Task<IdentityCrudStatus> DeleteRoleByIdAsync(Guid roleId,
        CancellationToken cancellationToken = default);
    Task<(IdentityCrudStatus Result, string Message)> UpdateRoleAsync(Guid roleId, string roleName,
        CancellationToken cancellationToken = default);

    Task<(IdentityCrudStatus Result, string Message)> AssignToUser(Role role, User user,
        CancellationToken cancellationToken = default);
}