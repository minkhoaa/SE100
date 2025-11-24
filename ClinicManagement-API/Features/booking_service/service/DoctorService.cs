using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClinicManagement_API.Domains.Entities;
using ClinicManagement_API.Features.booking_service.dto;
using ClinicManagement_API.Infrastructure.Persisstence;
using Microsoft.EntityFrameworkCore;

namespace ClinicManagement_API.Features.booking_service.service
{
    public interface IDoctorService
    {
        Task<IResult> CreateDoctorAsync(CreateDoctorRequest request);

        Task<IResult> GetAllDoctorAsync();

        Task<IResult> UpdateDoctorAsync(Guid doctorId, UpdateDoctorRequest request);

        Task<IResult> DeleteDoctorAsync(Guid doctorId);
    }
    public class DoctorService : IDoctorService
    {
        private readonly ClinicDbContext _context;

        public DoctorService(ClinicDbContext context)
        {
            _context = context;
        }

        public async Task<IResult> CreateDoctorAsync(CreateDoctorRequest request)
        {
            

            var doctor = new Doctor
            {
                ClinicId = request.ClinicId,
                Code = request.Code,
                FullName = request.FullName,
                Specialty = request.Specialty,
                Phone = request.Phone,
                Email = request.Email,
            };

            _context.Doctors.Add(doctor);
            await _context.SaveChangesAsync();

            var doctorDto = new DoctorDto(doctor.DoctorId, doctor.ClinicId, doctor.Code, doctor.FullName, doctor.Specialty, doctor.Phone, doctor.Email, doctor.IsActive);
            return Results.Created($"/doctors/{doctor.DoctorId}", new ApiResponse<DoctorDto>(true, "Doctor created successfully", doctorDto));
        }

        public async Task<IResult> GetAllDoctorAsync()
        {
            var doctors = await _context.Doctors.AsNoTracking()
            .Select(doctor => new DoctorDto(doctor.DoctorId, doctor.ClinicId, doctor.Code, doctor.FullName, doctor.Specialty, doctor.Phone, doctor.Email, doctor.IsActive))
            .ToListAsync();

            return Results.Ok(new ApiResponse<List<DoctorDto>>(true, "Doctor retrieved successfully", doctors));
        }

        public async Task<IResult> UpdateDoctorAsync(Guid doctorId, UpdateDoctorRequest request)
        {
            var affectedRows = await _context.Doctors.AsNoTracking().Where(x => x.DoctorId == doctorId).ExecuteUpdateAsync(x => x
                .SetProperty(a => a.FullName, request.FullName)
                .SetProperty(a => a.Specialty, request.Specialty)
                .SetProperty(a => a.Phone, request.Phone)
                .SetProperty(a => a.Email, request.Email)
            );
            return affectedRows > 0
                ? Results.Ok(new ApiResponse<object>(true, $"Doctor updated successfully {affectedRows}", null))
                : Results.NoContent();
        }

        public async Task<IResult> DeleteDoctorAsync(Guid doctorId)
        {
            var affectedRows = await _context.Doctors.AsNoTracking().Where(x => x.DoctorId == doctorId)
                .ExecuteDeleteAsync();
            return affectedRows > 0
                ? Results.Ok(new ApiResponse<object>(true, $"Deleted {affectedRows} row(s)", null))
                : Results.NoContent();
        }
    }
}