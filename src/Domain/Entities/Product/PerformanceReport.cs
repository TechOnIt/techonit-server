﻿using System;

namespace iot.Domain.Entities.Product;

public class PerformanceReport
{
    #region constructure

    public PerformanceReport(Guid id,int value,DateTime recordDateTime)
    {
        Id= id;
        Value= value;
        RecordDateTime= recordDateTime;
    }

    public PerformanceReport()
    {
        RecordDateTime = DateTime.Now;
    }
    #endregion

    public Guid Id { get; set; }
    public int Value { get; set; }
    public DateTime RecordDateTime { get; set; }

    #region relations
    //public virtual ICollection<Device> Devices { get; set; }
    
    #endregion
}
