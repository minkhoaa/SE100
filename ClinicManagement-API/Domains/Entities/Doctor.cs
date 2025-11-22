#nullable enable
using System;
using System.Collections.Generic;

namespace ClinicManagement_API.Domains.Entities;

public sealed class Doctor
{
    public Guid DoctorId { get; set; }
    public Guid ClinicId { get; set; }
    public string Code { get; set; } = default!;
    public string FullName { get; set; } = default!;
    public string? Specialty { get; set; }
    public string? Phone { get; set; }
    public string? Email { get; set; }
    public bool IsActive { get; set; } = true;

    public Clinic Clinic { get; set; } = default!;
    public ICollection<DoctorService> DoctorServices { get; set; } = new List<DoctorService>();
    public ICollection<DoctorAvailability> DoctorAvailabilities { get; set; } = new List<DoctorAvailability>();
    public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
    public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
}
