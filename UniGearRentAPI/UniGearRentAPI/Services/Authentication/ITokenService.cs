using Microsoft.AspNetCore.Identity;

namespace UniGearRentAPI.Services.Authentication;

public interface ITokenService
{
    public string CreateToken(IdentityUser user, string role);
}