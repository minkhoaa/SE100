public record CreateClinicRequest(
    Guid ClinicId,
    string Code,
    string Name,
    string? Phone,
    string? Email);

public record UpdateClinicRequest(
    string Name,
    string? Phone,
    string? Email);
