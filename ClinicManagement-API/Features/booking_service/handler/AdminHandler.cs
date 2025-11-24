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

    public static Task<IResult> CreateDoctor(IDoctorService svc, CreateDoctorRequest request)
        => svc.CreateDoctorAsync(request);

    public static Task<IResult> UpdateDoctor(IDoctorService svc, Guid clinicId, UpdateDoctorRequest request)
        => svc.UpdateDoctorAsync(clinicId, request);

    public static Task<IResult> DeleteDoctor(IDoctorService svc, Guid clinicId)
        => svc.DeleteDoctorAsync(clinicId);

    public static Task<IResult> GetAllDoctors(IDoctorService svc)
        => svc.GetAllDoctorAsync();
    public static Task<IResult> CreateAvailability(IUserService svc, CreateDoctorAvailability request)
        => svc.CreateAvailabilityAsync(request);
    public static Task<IResult> UpdateAvailability(IUserService svc, UpdateDoctorAvailability request)
        => svc.U(request);

}
