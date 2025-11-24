using ClinicManagement_API.Features.booking_service.dto;
using ClinicManagement_API.Features.booking_service.service;

namespace ClinicManagement_API.Features.booking_service.endpoint;

public static class ClinicEndpoint
{
    public static void MapClinicEndpoint(this IEndpointRouteBuilder route)
    {
        var app = route.MapGroup("/clinics").WithTags("Clinics Management");

        app.MapPost("/create-clinic", async (IClinicService svc, CreateClinicRequest request) =>
        {
            return await svc.CreateClinicAsync(request);
        });

        app.MapPut("update-clinic/{clinicId:guid}", async (IClinicService svc, Guid clinicId, UpdateClinicRequest request) =>
        {
            return await svc.UpdateClinicAsync(clinicId, request);
        });

        app.MapDelete("delete-clinic/{clinicId:guid}", async (IClinicService svc, Guid clinicId) =>
        {
            return await svc.DeleteClinicAsync(clinicId);
        });
    }
}
