﻿using System.Linq.Expressions;
using System.Reflection;
using TechOnIt.Application.Common.Models.ViewModels.Relay;
using TechOnIt.Application.Common.Models.ViewModels.Structures;
using TechOnIt.Application.Common.Models.ViewModels.Users;
using TechOnIt.Domain.Entities.Catalogs;
using TechOnIt.Domain.Entities.Identities.UserAggregate;

namespace TechOnIt.Application.Reports.Users;

public class UserReports
{
    #region constructor
    private readonly IUnitOfWorks _unitOfWorks;
    public UserReports(IUnitOfWorks unitOfWorks)
    {
        _unitOfWorks = unitOfWorks;
    }
    #endregion

    public async Task<UserViewModel?> FindByIdAsViewModelAsync(Guid id, CancellationToken cancellationToken)
        => await _unitOfWorks._context.Users
        .Where(u => u.Id == id)
        .ProjectToType<UserViewModel>()
        .FirstOrDefaultAsync(cancellationToken)
        ;

    public async Task<UserViewModel?> FindByIdNoTrackAsViewModelAsync(Guid id, CancellationToken cancellationToken)
        => await _unitOfWorks._context.Users
        .AsNoTracking()
        .Where(u => u.Id == id)
        .ProjectToType<UserViewModel>()
        .FirstOrDefaultAsync(cancellationToken)
        ;

    public async Task<UserViewModel?> FindByUsernameNoTrackAsViewModelAsync(string username, CancellationToken cancellationToken)
        => await _unitOfWorks._context.Users
        .AsNoTracking()
        .Where(u => u.Username == username)
        .ProjectToType<UserViewModel>()
        .FirstOrDefaultAsync(cancellationToken)
        ;

    public async Task<UserEntity?> FindByIdentityNoTrackAsync(string identity, CancellationToken cancellationToken)
        => await _unitOfWorks._context.Users
        .AsNoTracking()
        .FirstOrDefaultAsync(u => u.Email == identity || u.PhoneNumber == identity || u.Username == identity, cancellationToken);

    /// <summary>
    /// try to do not use of this method when you have less than 1000 users in database for better perfomance
    /// </summary>
    /// <returns>IList<UserViewModel></returns>
    public async Task<IList<UserViewModel>> GetAllUsersParallel()
        => _unitOfWorks._context.Users.AsNoTracking()
        .AsParallel()
        .WithDegreeOfParallelism(3)
        .Adapt<IList<UserViewModel>>();

    /// <summary>
    /// use this method when you have less than 200 users in database
    /// </summary>
    /// <returns>IList<UserViewModel></returns>
    public IList<UserViewModel> GetAllUsersSync()
        => _unitOfWorks._context.Users.AsNoTracking().ToList().Adapt<IList<UserViewModel>>();

    public async Task<IList<UserViewModel>> GetAllUsersAsync()
    {
        var users = await _unitOfWorks._context.Users.AsNoTracking().ToListAsync();
        return users.Adapt<IList<UserViewModel>>();
    }

    public async Task<IList<UserViewModel>> GetByConditionAsync(Expression<Func<UserEntity, bool>> filter = null,
        Func<IQueryable, IOrderedQueryable<UserEntity>> orderBy = null,
    params Expression<Func<UserEntity, object>>[] includes)
    {
        IQueryable<UserEntity> query = _unitOfWorks._context.Users;
        foreach (var include in includes)
        {
            query = query.Include(include);
        }

        if (filter != null)
        {
            query = query.Where(filter);
        }

        if (orderBy != null)
        {
            query = orderBy(query);
        }

        var executionQueries = await query.AsNoTracking().ToListAsync();
        return executionQueries.Adapt<IList<UserViewModel>>();
    }

    /// <summary>
    /// Apply condition, specify the viewmodel and pagination.
    /// </summary>
    /// <typeparam name="TDestination">Type of view model.</typeparam>
    /// <param name="config">Config for mapster.</param>
    /// <returns>The output you specified yourself!</returns>
    public async Task<PaginatedList<TDestination>> GetAllPaginatedSearchAsync<TDestination>(PaginatedSearchWithSize paginatedSearch,
        TypeAdapterConfig? config = null, CancellationToken cancellationToken = default)
    {
        // Initialize users Query.
        var query = _unitOfWorks._context.Users.AsQueryable();
        // Apply conditions.
        if (!string.IsNullOrEmpty(paginatedSearch.Keyword))
            query = query
                .Where(u => paginatedSearch.Keyword.Contains(u.Username) ||
                paginatedSearch.Keyword.Contains(u.PhoneNumber) ||
                paginatedSearch.Keyword.Contains(u.Email) ||
                paginatedSearch.Keyword.Contains(u.FullName.Name) ||
                paginatedSearch.Keyword.Contains(u.FullName.Surname))
                .AsQueryable();
        // Execute, pagination and type to project.
        return await query
            .AsNoTracking()
            .PaginatedListAsync<UserEntity, TDestination>(paginatedSearch.Page, paginatedSearch.PageSize, config, cancellationToken);
    }

    public async Task<IList<UserViewModel>> GetUsersInRoleAsync(string roleName, Guid? roleId = null)
    {
        IQueryable<UserEntity> query = null;
        IList<UserEntity> users = new List<UserEntity>();

        if (roleId == null)
        {
            var getRole = await _unitOfWorks._context.Roles.AsNoTracking().FirstOrDefaultAsync(a => a.Name == roleName);
            if (getRole == null)
                return new List<UserViewModel>();

            Guid[] allUsersInRole = await _unitOfWorks._context.UserRoles.AsNoTracking().Where(a => a.RoleId == getRole.Id)
                .Select(a => a.UserId).ToArrayAsync();

            if (allUsersInRole.Length > 0)
            {
                query = _unitOfWorks._context.Users.AsNoTracking()
                   .Where(a => allUsersInRole.Any(x => x == a.Id) == true);

                users = await query.ToListAsync();
            }
        }
        else
        {
            Guid[] usersInRole = await _unitOfWorks._context.UserRoles.AsNoTracking().Where(a => a.RoleId == roleId)
                .Select(a => a.UserId).ToArrayAsync();

            if (usersInRole.Length > 0)
            {
                query = _unitOfWorks._context.Users.AsNoTracking()
                   .Where(a => usersInRole.Any(x => x == a.Id) == true);

                users = await query.ToListAsync();
            }
        }

        return users.Adapt<IList<UserViewModel>>();
    }
    public async Task<IList<StructureViewModel>?> GetUserStructuresByUserIdAsync(Guid userId)
    {
        try
        {
            var user = await _unitOfWorks.UserRepository.FindByIdAsNoTrackingAsync(userId);
            if (user == null)
                return null;

            Expression<Func<StructureEntity, bool>> getStructures = a => a.UserId == user.Id;
            var cancellationSource = new CancellationToken();

            var structures = await _unitOfWorks.StructureRepository.GetAllByFilterAsync(cancellationSource, getStructures);
            if (structures is null)
                return null;

            return structures.Adapt<IList<StructureViewModel>>();
        }
        catch
        {
            return null;
        }
    }
    public async Task<IList<RelayViewModel>?> GetAllRelaysByUserIdAsync(Guid userId)
    {
        try
        {
            var relays = await (from str in _unitOfWorks._context.Structures
                                join grp in _unitOfWorks._context.Groups on str.Id equals grp.StructureId
                                into groups
                                from pl in groups.DefaultIfEmpty()
                                join rel in _unitOfWorks._context.Relays on pl.Id equals rel.GroupId
                                into relay
                                from de in relay.DefaultIfEmpty()
                                where str.UserId == userId
                                select new RelayViewModel
                                {
                                    Id = de.Id,
                                    Pin = de.Pin,
                                    RelayType = de.Type,
                                    IsHigh = de.IsHigh,
                                    GroupId = de.GroupId,

                                }).AsNoTracking().ToListAsync();
            return relays;
        }
        catch (ReportExceptions exp)
        {
            throw new ReportExceptions($"error in geting all user relays by user Id : {exp.UserId}");
        }
    }

    public PropertyInfo? GetUserProperty(string propertyName)
    {
        // get all public static properties of MyClass type
        PropertyInfo[] propertyInfos;
        propertyInfos = typeof(UserEntity)
            .GetProperties(BindingFlags.Public | BindingFlags.Static);
        // sort properties by name
        Array.Sort(propertyInfos,
                delegate (PropertyInfo propertyInfo1, PropertyInfo propertyInfo2)
                { return propertyInfo1.Name.CompareTo(propertyInfo2.Name); });
        PropertyInfo? result = null;
        // write property names
        foreach (PropertyInfo propertyInfo in propertyInfos)
        {
            if (propertyInfo.Name.ToLower().Contains(propertyName.ToLower()))
                result = propertyInfo;

            if (result != null)
                if (!string.IsNullOrWhiteSpace(result.Name))
                    break;
        }
        return result;
    }

    public async Task<object> GetNewUsersCountGroupbyRegisterDateAsync(DateTime from, CancellationToken cancellationToken)
    {
        return await _unitOfWorks._context.Users
            .AsNoTracking()
            .Where(user => user.RegisteredAt > from)
            .GroupBy(user => user.RegisteredAt.Month)
            .Select(u => new
            {
                month = u.First().RegisteredAt.ToString("MMM"),
                count = u.Count()
            })
            .ToListAsync(cancellationToken);
    }
}