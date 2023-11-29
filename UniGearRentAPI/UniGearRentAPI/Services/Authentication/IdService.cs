using Microsoft.AspNetCore.Identity;

namespace UniGearRentAPI.Services.Authentication;

public class IdService : IIdService
{
    private readonly UserManager<IdentityUser> _userManager;

    public IdService(UserManager<IdentityUser> userManager)
    {
        _userManager = userManager;
    }
    public string GetId(string userName, string email, string phoneNumber)
    {
        return _userManager.Users.ToList().First(user =>
            user.UserName == userName && user.Email == email && user.PhoneNumber == phoneNumber).Id;
    }
}