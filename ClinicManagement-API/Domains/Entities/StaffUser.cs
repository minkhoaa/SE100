using ClinicManagement_API.Domains.Enums;

namespace ClinicManagement_API.Domains.Entities
{
    public class StaffUser
    {
        public Guid UserId { get; set; }
        public Guid ClinicId { get; set; }
        public string Username { get; set; } = default!;
        public string FullName { get; set; } = default!;
        public StaffRole Role { get; set; } = StaffRole.Receptionist;
        public bool IsActive { get; set; } = true;
        public Clinic Clinic { get; set; } = default!;
    }
}