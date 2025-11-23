using ClinicManagement_API.Features.booking_service.dto;
using ClinicManagement_API.Features.booking_service.service;

namespace ClinicManagement_API.Features.booking_service.endpoint;

public static class BookingEndpoint
{
    public static void MapUserEndpoint(this IEndpointRouteBuilder route)
    {
        var app = route.MapGroup("");

        app.MapGet("/clinics", async (IUserService svc, string? nameOrCode) =>
        {
            return await svc.GetClinicsAsync(nameOrCode);
        });

        app.MapGet("/services", async (IUserService svc, Guid? clinicId, string? nameOrCode, bool? isActive) =>
        {
            return await svc.GetServicesAsync(clinicId, nameOrCode, isActive);
        });

        app.MapGet("/doctors", async (IUserService svc, Guid? clinicId, string? nameOrCode, string? specialty, Guid? serviceId, bool? isActive) =>
        {
            return await svc.GetDoctorsAsync(clinicId, nameOrCode, specialty, serviceId, isActive);
        });

        app.MapGet("/doctors/{doctorId:guid}/availability", (IUserService svc, Guid doctorId, DateOnly from, DateOnly to)
            => svc.GetAvailabilityAsync(doctorId, from, to));

        app.MapGet("/slots", (IUserService svc, Guid clinicId, Guid doctorId, Guid? serviceId, DateOnly date)
            => svc.GetSlotsAsync(clinicId, doctorId, serviceId, date));

        app.MapPost("/bookings", (IUserService svc, CreateBookingRequest req)
            => svc.CreateBookingAsync(req));

        app.MapGet("/bookings/{bookingId:guid}", (IUserService svc, Guid bookingId)
            => svc.GetBookingAsync(bookingId));

        app.MapPost("/bookings/{bookingId:guid}/confirm", (IUserService svc, Guid bookingId)
            => svc.ConfirmBookingAsync(bookingId));
    }

    public static void MapAdminEndpoint(this IEndpointRouteBuilder route)
    {
        // Reserved for future admin APIs
    }
}
