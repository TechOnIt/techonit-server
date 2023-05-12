﻿using TechOnIt.Domain.Entities.SensorAggregate;

namespace TechOnIt.Infrastructure.Repositories.SQL.SensorAggregate;

public interface ISensorRepository
{
    #region Sensor
    Task CreateSensorAsync(Sensor sensor, CancellationToken cancellationToken = default);
    Task<(bool Result, bool IsExists)> UpdateSensorAsync(Sensor sensor, CancellationToken cancellationToken);
    Task<(bool Result, bool IsExists)> DeleteSensorByIdAsync(Guid sensorId, CancellationToken cancellationToken);
    Task<Sensor?> GetSensorByIdAsync(Guid sensorId, CancellationToken cancellationToken = default);
    Task<IList<Sensor>> GetAllSensorsByPlaceIdAsync(Guid placeId, CancellationToken cancellationToken);
    Task<IList<SensorReport>?> GetSensorReportBySensorIdAsNoTrackingAsync(Guid sensorId, CancellationToken cancellationToken);
    Task<IList<SensorReport>?> GetSensorReportBySensorIdAsNoTrackingWithTimeFilterAsync(Guid sensorId, DateTime minTime, DateTime maxTime, CancellationToken cancellationToken);
    #endregion

    #region Report
    Task<IList<SensorReport>?> GetSensorReportBySensorIdAsync(Guid sensorId, CancellationToken cancellationToken = default);
    Task<SensorReport?> FindRepoprtByIdAsync(Guid reportId, CancellationToken cancellationToken = default);
    Task AddReportToSensorAsync(SensorReport model, CancellationToken cancellationToken);
    #endregion
}