using ClinicManagement_API.Features.booking_service.dto;
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

    public static Task<IResult> UpdateDoctor(IDoctorService svc, Guid doctorId, UpdateDoctorRequest request)
        => svc.UpdateDoctorAsync(doctorId, request);

    public static Task<IResult> DeleteDoctor(IDoctorService svc, Guid doctorId)
        => svc.DeleteDoctorAsync(doctorId);

    public static Task<IResult> GetAllDoctors(IDoctorService svc)
        => svc.GetAllDoctorAsync();
    
    public static Task<IResult> CreateService(IServiceService svc, CreateServiceRequest request)
        => svc.CreateService(request);
    public static Task<IResult> CreateAvailability(IUserService svc, CreateDoctorAvailability request)
        => svc.CreateAvailabilityAsync(request);
    public static Task<IResult> UpdateAvailability(IUserService svc, UpdateDoctorAvailability request)
        => svc.U(request);

    public static Task<IResult> UpdateService(IServiceService svc, Guid serviceId, CreateServiceRequest request)
        => svc.UpdateService(serviceId, request);

    public static Task<IResult> DeleteService(IServiceService svc, Guid serviceId)
        => svc.DeleteService(serviceId);

    public static Task<IResult> GetAllServices(IServiceService svc)
        => svc.GetAllService();
    
}
