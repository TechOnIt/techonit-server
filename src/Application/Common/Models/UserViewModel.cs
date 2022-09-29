﻿using iot.Domain.ValueObjects;

namespace iot.Application.Common.Models;

// I Removed {} for Namespace because we its better to do in c# 10 and .net 6
// https://dotnetcoretutorials.com/2021/09/20/file-scoped-namespaces-in-c-10/

public class UserViewModel
{

    public UserViewModel()
    {

    }

    /// <summary>
    /// constructor for sign up
    /// </summary>
    /// <param name="Username"></param>
    /// <param name="phonenumber"></param>
    public UserViewModel(string username,string phonenumber,string password)
    {
        this.Username = username;
        this.PhoneNumber = phonenumber;
        this.Password = PasswordHash.Parse(password);
        ConfirmedEmail = false;
        ConfirmedPhoneNumber = false;
        RegisteredDateTime = DateTime.Now;
        IsBaned = false;
        IsDeleted = false;
        MaxFailCount = 3;
    }

    public Guid Id { get; set; } = Guid.NewGuid();
    public string? Username { get; private set; }
    public string? Name { get; set; }
    public string? Surname { get; set; }
    public string? Email { get; private set; }
    public string? PhoneNumber { get; private set; }
    public bool ConfirmedEmail { get; private set; }
    public bool ConfirmedPhoneNumber { get; private set; }
    public DateTime RegisteredDateTime { get; private set; }
    public DateTime? LockOutDateTime { get; private set; }
    public bool IsBaned { get; set; }
    public bool IsDeleted { get; set; }
    public short MaxFailCount { get; private set; }

    public Concurrency ConcurrencyStamp { get; private set; }
    public PasswordHash Password { get; private set; } 
}
