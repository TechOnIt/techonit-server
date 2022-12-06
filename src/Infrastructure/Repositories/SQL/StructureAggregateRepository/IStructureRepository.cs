﻿using iot.Domain.Entities.Product.StructureAggregate;
using System.Linq.Expressions;

namespace iot.Infrastructure.Repositories.SQL.StructureAggregateRepository;

public interface IStructureRepository
{
    #region structure
    Task CreateStructureAsync(Structure structure, CancellationToken cancellationToken = default);
    Task UpdateStructureAsync(Structure structure, CancellationToken cancellationToken = default);
    Task DeleteStructure(Structure structure, CancellationToken cancellationToken);
    Task DeleteStructureAsync(Guid structureId, CancellationToken cancellationToken = default);
    Task<IList<Structure>> GetStructuresByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);
    Task<Structure> GetStructureByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);
    Task<Structure> GetStructureByIdAsync(Guid structureId, CancellationToken cancellationToken = default);
    Task<Structure?> GetStructureByIdAsyncAsNoTracking(Guid structureId, CancellationToken cancellationToken = default);
    Task<IList<Structure>> GetAllStructuresByFilterAsync(CancellationToken cancellationToken, Expression<Func<Structure, bool>> filter = null);
    #endregion

    #region commen
    Task CreateStructureWithPlacesAsync(Structure structure, IList<Place> places, CancellationToken cancellationToken = default);
    Task<IList<Place>> GetPlacesByStructureIdAsync(Guid structureId, CancellationToken cancellationToken = default);
    #endregion

    #region place
    Task CreatePlaceAsync(Place place, Guid StructureId, CancellationToken cancellationToken = default);
    Task UpdatePlaceAsync(Guid structureId, Place place, CancellationToken cancellationToken = default);
    Task DeletePlaceAsync(Guid structureId, Place place, CancellationToken cancellationToken = default);
    Task<Place> GetPlaceByIdAsync(Guid placeId, CancellationToken cancellationToken = default);
    Task<Place?> GetPlaceByIdAsyncAsNoTracking(Guid placeId, CancellationToken cancellationToken = default);
    Task<IList<Place>> GetAllPlcaesByFilterAsync(CancellationToken cancellationToken, Expression<Func<Place, bool>> filter = null);
    #endregion
}
