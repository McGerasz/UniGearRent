namespace UniGearRentAPI.Contracts;

public record RegistrationResponse(
    string Email,
    string UserName,
    string PhoneNumber);