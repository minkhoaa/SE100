public record CreateDoctorRequest(
    Guid ClinicId,
    string Code,
    string FullName,
    string? Specialty,
    string? Phone,
    string? Email);

public record UpdateDoctorRequest(
    Guid CLinicId,
    string Code,
    string FullName,
    string? Specialty,
    string? Phone,
    string? Email);
