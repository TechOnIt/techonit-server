﻿using iot.Domain.Entities.Product.StructureAggregate;
using iot.Domain.Enums;
using System;

namespace iot.Domain.Entities.Product;

public class Device
{
    #region constructure
    public Device(int pin, DeviceType deviceType, bool isHigh, Guid placeId)
    {
        Id = Guid.NewGuid();
        Pin = pin;  
        DeviceType = deviceType;    
        IsHigh = isHigh;
        PlaceId = placeId;
    }

    public Device()
    {
    }
    #endregion

    private Guid? _Id;
    public Guid Id 
    {
        get { return _Id ?? Guid.NewGuid(); }
        private set { _Id = value; }
    }


    public int Pin { get; set; }
    public DeviceType DeviceType { get; private set; }
    public bool IsHigh { get; set; }

    //Concurrency : from entityframework
    public byte[] RowVersion { get; set; }

    #region methods

    public Guid GetDeviceId()
    {
        return this.Id;
    }

    public void SetDeviceType(string typeName)
    {
        DeviceType = DeviceType.FromDisplayName<DeviceType>(typeName);
    }

    public void SetDeviceType(int value)
    {
        DeviceType = DeviceType.FromValue<DeviceType>(value);
    }

    public void SetDeviceType(DeviceType deviceType)
    {
        DeviceType = deviceType;
    }

    public DeviceType GetDeviceType()
        => this.DeviceType;
    #endregion

    #region relations & foreignkeys
    public Guid PlaceId { get; set; }
    public Place Place { get; set; }
    #endregion
}
