using ClinicManagement_API.Features.booking_service.service;

namespace ClinicManagement_API.Features.booking_service.handler;

public static class AdminHandler
{
    public static Task<IResult> CreateClinic(IClinicService svc, CreateClinicRequest request)
        => svc.CreateClinicAsync(request);

    public static Task<IResult> UpdateClinic(IClinicService svc, Guid clinicId, UpdateClinicRequest request)
        => svc.UpdateClinicAsync(clinicId, request);

    public static Task<IResult> DeleteClinic(IClinicService svc, Guid clinicId)
        => svc.DeleteClinicAsync(clinicId);

    public static Task<IResult> GetAllClinics(IClinicService svc)
        => svc.GetAllClinicAsync();
}
