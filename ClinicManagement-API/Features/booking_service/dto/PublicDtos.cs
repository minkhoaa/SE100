#nullable enable
using ClinicManagement_API.Domains.Enums;

namespace ClinicManagement_API.Features.booking_service.dto;

public record ClinicDto(Guid ClinicId, string Code, string Name, string TimeZone, string? Phone, string? Email);
public record ServiceDto(Guid ServiceId, string Code, string Name, short? DefaultDurationMin, decimal? DefaultPrice, bool IsActive, Guid ClinicId);
public record DoctorDto(Guid DoctorId, Guid ClinicId, string Code, string FullName, string? Specialty, string? Phone, string? Email, bool IsActive);
public record AvailabilityDto(DateOnly Date, TimeSpan StartTime, TimeSpan EndTime, short SlotSizeMin);
public record SlotDto(DateTime StartAt, DateTime EndAt);
public record ApiResponse<T>(bool IsSuccess, string Message, T? Data);

public record CreateBookingRequest(
    Guid ClinicId,
    Guid DoctorId,
    Guid? ServiceId,
    DateTime StartAt,
    DateTime EndAt,
    string FullName,
    string Phone,
    string? Email,
    string? Notes,
    string? Channel);

public record BookingResponse(Guid BookingId, BookingStatus Status, string? CancelToken, string? RescheduleToken);
public record AppointmentResponse(Guid AppointmentId, AppointmentStatus Status);