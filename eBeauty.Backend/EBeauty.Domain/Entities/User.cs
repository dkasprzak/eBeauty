﻿using EBeauty.Domain.Common;

namespace EBeauty.Domain.Entities;

public class User : DomainEntity
{
    public required string Email { get; set; }
    public required string HashedPassword { get; set; }
    public DateTimeOffset RegisterDate { get; set; }
    public string FirstName { get; set; } = "";
    public string LastName { get; set; } = "";
    public bool IsActive { get; set; }
    public ICollection<AccountUser> AccountUsers { get; set; } = new List<AccountUser>();
    public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
}
