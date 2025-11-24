using ClinicManagement_API.Features.booking_service.handler;

namespace ClinicManagement_API.Features.booking_service.endpoint;

public static class DoctorEndpoint
{
    public static void MapDoctorEndpoint(this IEndpointRouteBuilder route)
    {
        var app = route.MapGroup("/api/admin/doctor").WithTags("Doctors Management");
        app.MapPost("/", AdminHandler.CreateDoctor);
        app.MapPut("/{doctorId:guid}", AdminHandler.UpdateDoctor);
        app.MapDelete("/{doctorId:guid}", AdminHandler.DeleteDoctor);
        app.MapGet("/", AdminHandler.GetAllDoctors);
    }
}
