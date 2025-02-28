﻿using System.Security.Claims;
using TechOnIt.Domain.Entities.Catalogs;
using TechOnIt.Domain.Entities.Identities.UserAggregate;

namespace TechOnIt.Application.Common.Extentions;

public static class AuthorizationExtentions
{
    public static async Task<IEnumerable<Claim>> GetClaims(this UserEntity user)
    {
        IList<Claim> claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.MobilePhone, user.PhoneNumber)
        };

        if (user.FullName is not null)
        {
            if (!string.IsNullOrEmpty(user.FullName.Name))
                claims.Add(new Claim(ClaimTypes.GivenName, user.FullName.Name));
            if (!string.IsNullOrEmpty(user.FullName.Surname))
                claims.Add(new Claim(ClaimTypes.Surname, user.FullName.Surname));
        }

        if (user.UserRoles != null && user.UserRoles.Count > 0)
        {
            foreach (var userRole in user.UserRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, userRole.Role.Name));
            }
        }

        return await Task.FromResult(claims);
    }
    public static async Task<IEnumerable<Claim>> GetIdAsClaim(this UserEntity user)
    {
        IList<Claim> claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
        };

        return await Task.FromResult(claims);
    }


    public static async Task<IEnumerable<Claim>> GetClaims(this StructureEntity structure)
    {
        IList<Claim> claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, $"{structure.Name}"),
            new Claim(ClaimTypes.NameIdentifier, structure.Id.ToString()),
            new Claim(ClaimTypes.SerialNumber, structure.ApiKey.Value.ToString()),
        };

        return await Task.FromResult(claims);
    }

    public static async Task<IEnumerable<Claim>> GetIdAsClaim(this StructureEntity structure)
    {
        IList<Claim> claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, structure.Id.ToString()),
        };

        return await Task.FromResult(claims);
    }
}
