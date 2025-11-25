using ClinicManagement_API.Features.booking_service.handler;

namespace ClinicManagement_API.Features.booking_service.endpoint;

public static class ClinicEndpoint
{
    public static void MapClinicEndpoint(this IEndpointRouteBuilder route)
    {
        var app = route.MapGroup("/api/admin/clinic").WithTags("Clinics Management");
        app.MapPost("/", AdminHandler.CreateClinic);
        app.MapPut("/{clinicId:guid}", AdminHandler.UpdateClinic);
        app.MapDelete("/{clinicId:guid}", AdminHandler.DeleteClinic);
        app.MapGet("/", AdminHandler.GetAllClinics);
    }
}
