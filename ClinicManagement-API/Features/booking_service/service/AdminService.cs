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
    
    public interface IAdminService
    {
        Task<IResult> CreateStaffAsync(CreateStaffUserDto request);
            
        Task<IResult> UpdateStaffAsync(Guid userId, CreateStaffUserDto request);
            
        Task<IResult> DeleteStaffAsync(Guid userId);
    }
    public class AdminService : IAdminService
    {
        private readonly ClinicDbContext _context;
        public AdminService(ClinicDbContext context)
        {
            _context = context;
        }
        
        public async Task<IResult> CreateStaffAsync(CreateStaffUserDto request)
        {
            var existingClinic = await _context.Clinics.AsNoTracking().AnyAsync(c => c.ClinicId == request.ClinicId);
            if (existingClinic)
            {
                return Results.NotFound(new ApiResponse<object>(false, "Clinic not found", existingClinic));
            }
            
            var staff = new StaffUser
            {

                ClinicId = request.ClinicId,
                Username = request.Username,
                FullName = request.FullName,
                IsActive = request.IsActive,
                Role = request.Role
            };

            _context.StaffUsers.Add(staff);
            await _context.SaveChangesAsync();
            
            var staffUserDto = new StaffUserDto(staff.UserId, staff.ClinicId, staff.Username, staff.FullName, staff.Role,staff.IsActive, staff.Clinic);
            return Results.Created($"/staff/{staff.UserId}", new ApiResponse<StaffUserDto>(true, "Staff created successfully", staffUserDto));
        }

        public async Task<IResult> UpdateStaffAsync(Guid userId, CreateStaffUserDto request)
        {
            var existingClinic = await _context.Clinics.AsNoTracking().AnyAsync(c => c.ClinicId == request.ClinicId);
            if (!existingClinic)
            {
                return Results.NotFound(new ApiResponse<object>(false, "Clinic not found", existingClinic));
            }
            
            var existingStaff = await _context.StaffUsers.AsNoTracking().AnyAsync(c => c.UserId == userId);
            if (!existingStaff)
            {
                return Results.NotFound(new ApiResponse<object>(false, "Staff not found", existingStaff));
            }
            
            var affectedRows = await _context.StaffUsers.AsNoTracking().Where(s => s.UserId == userId).ExecuteUpdateAsync<StaffUser>(s => s
                .SetProperty(s => s.ClinicId, request.ClinicId)
                .SetProperty(s => s.Username, request.Username)
                .SetProperty(s => s.FullName, request.FullName)
                .SetProperty(s => s.Role, request.Role)
                .SetProperty(s => s.IsActive, request.IsActive));
            return affectedRows > 0
                ? Results.Ok(new ApiResponse<object>(true, "Staff updated successfully",  affectedRows))
                : Results.NoContent();
        }

        public async Task<IResult> DeleteStaffAsync(Guid userId)
        {
            var existingStaff = await _context.StaffUsers.AsNoTracking().AnyAsync(c => c.UserId == userId);
            if (!existingStaff)
            {
                return Results.NotFound(new ApiResponse<object>(false, "Staff not found", existingStaff));
            }
            
            var affectedRows = await _context.StaffUsers.AsNoTracking().Where(x => x.UserId == userId)
                .ExecuteDeleteAsync();
            return affectedRows > 0
                ? Results.Ok(new ApiResponse<object>(true, $"Deleted {affectedRows} row(s)", null))
                : Results.NoContent();
        }
    }
}