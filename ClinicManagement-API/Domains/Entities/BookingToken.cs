#nullable enable
using System;

namespace ClinicManagement_API.Domains.Entities;

public sealed class BookingToken
{
    public Guid BookingId { get; set; }
    public string Action { get; set; } = default!;
    public string Token { get; set; } = default!;
    public DateTime ExpiresAt { get; set; }

    public Booking Booking { get; set; } = default!;
}
