#nullable enable
using System;

namespace ClinicManagement_API.Domains.Entities;

public sealed class DoctorService
{
    public Guid ServiceId { get; set; }
    public Guid DoctorId { get; set; }
    public bool IsEnabled { get; set; } = true;

    public Service Service { get; set; } = default!;
    public Doctor Doctor { get; set; } = default!;
}
