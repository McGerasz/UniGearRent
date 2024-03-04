namespace UniGearRentAPI.Contracts;

public record AuthResponse(string Email, string UserName, string PhoneNumber, string Token, string Id);