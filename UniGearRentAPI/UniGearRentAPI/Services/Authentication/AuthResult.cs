namespace UniGearRentAPI.Services.Authentication;

public record AuthResult(
    bool Success,
    string UserId,
    string Email,
    string UserName,
    string PhoneNumber,
    string Token)
{
    public readonly Dictionary<string, string> ErrorMessages = new();
}