
using ClinicManagement_API.Domains.Entities;
using ClinicManagement_API.Features.booking_service.dto;
using ClinicManagement_API.Infrastructure.Persisstence;
using Microsoft.EntityFrameworkCore;

namespace ClinicManagement_API.Features.booking_service.service
{

    public interface IClinicService
    {

        Task<IResult> CreateClinicAsync(CreateClinicRequest request);
        Task<IResult> UpdateClinicAsync(Guid clinicId, UpdateClinicRequest request);
        Task<IResult> DeleteClinicAsync(Guid clinicId);
    }

    public class ClinicService : IClinicService
    {
        private readonly ClinicDbContext _context;

        public ClinicService(ClinicDbContext context)
        {
            _context = context;
        }

        public async Task<IResult> CreateClinicAsync(CreateClinicRequest request)
        {
            var existingClinic = await _context.Clinics.FirstOrDefaultAsync(c => c.Code == request.Code);
            if (existingClinic != null)
            {
                return Results.Conflict(new ApiResponse<object>(false, $"Clinic with code '{request.Code}' already exists.", null));
            }

            var clinic = new Clinic
            {
                Code = request.Code,
                Name = request.Name,
                Phone = request.Phone,
                Email = request.Email,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            _context.Clinics.Add(clinic);
            await _context.SaveChangesAsync();

            var clinicDto = new ClinicDto(clinic.ClinicId, clinic.Code, clinic.Name, clinic.TimeZone, clinic.Phone, clinic.Email);
            return Results.Created($"/clinics/{clinic.ClinicId}", new ApiResponse<ClinicDto>(true, "Clinic created successfully", clinicDto));
        }

        public async Task<IResult> UpdateClinicAsync(Guid clinicId, UpdateClinicRequest request)
        {
            var clinic = await _context.Clinics.FindAsync(clinicId);

            if (clinic == null)
            {
                return Results.NotFound(new ApiResponse<object>(false, "Clinic not found", null));
            }

            // Update properties
            clinic.Name = request.Name;
            clinic.Phone = request.Phone;
            clinic.Email = request.Email;
            clinic.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return Results.Ok(new ApiResponse<object>(true, "Clinic updated successfully", null));
        }

        public async Task<IResult> DeleteClinicAsync(Guid clinicId)
        {
            var clinic = await _context.Clinics.FindAsync(clinicId);

            if (clinic == null)
            {
                return Results.NotFound(new ApiResponse<object>(false, "Clinic not found", null));
            }

            _context.Clinics.Remove(clinic);
            await _context.SaveChangesAsync();

            return Results.NoContent();
        }
    }
}
