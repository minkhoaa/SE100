using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClinicManagement_API.Infrastructure.Persisstence;

namespace ClinicManagement_API.Features.booking_service.service
{
    public interface IAdminService
    {

    }
    public class AdminService : IAdminService
    {
        private readonly ClinicDbContext _context;
        public AdminService(ClinicDbContext context)
        {
            _context = context;
        }
    }
}