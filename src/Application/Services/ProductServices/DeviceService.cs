﻿using iot.Application.Services.ProductServices.ProductContracts;
using iot.Domain.Entities.Product;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace iot.Application.Services.ProductServices;

public class DeviceService : IDeviceService
{
    #region constructor
    private readonly IUnitOfWorks _unitOfWorks;
    public DeviceService(IUnitOfWorks unitOfWorks)
    {
        _unitOfWorks = unitOfWorks;
    }
    #endregion

    public async Task<DeviceViewModel?> CreateDeviceAsync(DeviceViewModel device, CancellationToken cancellationToken)
    {
        var createResult = await _unitOfWorks.DeviceRepositry.CreateDeviceAsync(device.Adapt<Device>(),cancellationToken);

        if (createResult is null)
            return await Task.FromResult<DeviceViewModel?>(null);

        return await Task.FromResult<DeviceViewModel?>(createResult.Adapt<DeviceViewModel>());
    }
    public async Task<bool> DeleteDeviceByIdAsync(Guid DeviceId, CancellationToken cancellationToken)
    {
        var deleteResult= _unitOfWorks.DeviceRepositry.DeleteDeviceByIdAsync(DeviceId,cancellationToken).IsCompletedSuccessfully;

        if (deleteResult is not true)
            return await Task.FromResult(false);

        return await Task.FromResult(true);    
    }
    public async Task<DeviceViewModel?> UpdateDeviceAsync(DeviceViewModel device, CancellationToken cancellationToken)
    {
        var getrecentDevice = await _unitOfWorks.DeviceRepositry.FindDeviceByIdAsync(device.Id,cancellationToken);
        if (getrecentDevice is null)
            return await Task.FromResult<DeviceViewModel?>(null);

        getrecentDevice.Pin = device.Pin;
        getrecentDevice.IsHigh = device.IsHigh;
        getrecentDevice.SetDeviceType(device.DeviceType);

        var updateResult = await _unitOfWorks.DeviceRepositry.UpdateDeviceAsync(getrecentDevice, cancellationToken);
        if (updateResult is null)
            return await Task.FromResult<DeviceViewModel?>(null);

        return await Task.FromResult(updateResult.Adapt<DeviceViewModel>());
    }
    public async Task<DeviceViewModel?> FindDeviceByIdAsync(Guid deviceId, CancellationToken cancellationToken)
    {
        var getDevice = await _unitOfWorks.DeviceRepositry.FindDeviceByIdAsync(deviceId,cancellationToken);
        if (getDevice is null)
            return await Task.FromResult<DeviceViewModel?>(null);

       return await Task.FromResult<DeviceViewModel?>(getDevice.Adapt<DeviceViewModel>());
    }
    public async Task<DeviceViewModel?> FindDeviceByIdAsyncAsNoTracking(Guid deviceId, CancellationToken cancellationToken)
    {
        var getDevice = await _unitOfWorks.DeviceRepositry.FindDeviceByIdAsyncAsNoTracking(deviceId, cancellationToken);
        if (getDevice is null)
            return await Task.FromResult<DeviceViewModel?>(null);

        return await Task.FromResult<DeviceViewModel?>(getDevice.Adapt<DeviceViewModel>());
    }

}
