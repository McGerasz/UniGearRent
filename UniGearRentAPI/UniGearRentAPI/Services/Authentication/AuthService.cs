using Microsoft.AspNetCore.Identity;
using UniGearRentAPI.DatabaseServices;

namespace UniGearRentAPI.Services.Authentication;

public class AuthService : IAuthService
{
    private readonly ITokenService _tokenService;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly UniGearRentAPIDbContext _dbContext;

    public AuthService(UserManager<IdentityUser> userManager, ITokenService tokenService, UniGearRentAPIDbContext dbContext)
    {
        _userManager = userManager;
        _tokenService = tokenService;
        _dbContext = dbContext;
    }

    public async Task<AuthResult> RegisterAsync(string email, string username, string password, string phoneNumber, string role)
    {
        var user = new IdentityUser { UserName = username, Email = email, PhoneNumber = phoneNumber};
        var result = await _userManager.CreateAsync(user, password);

        if (!result.Succeeded)
            return FailedRegistration(result, email, username, phoneNumber);
        
        await _userManager.AddToRoleAsync(user, role);
        var id = _userManager.Users.ToList().First(userInQuery =>
            userInQuery.UserName == username && userInQuery.Email == email && userInQuery.PhoneNumber == phoneNumber).Id;
        return new AuthResult(true, id,email, username, phoneNumber, "");
    }

    public async Task<AuthResult> LoginAsync(string email, string password)
    {
        var managedUser = await _userManager.FindByEmailAsync(email);

        if (managedUser == null)
            return InvalidEmail(email);

        var isPasswordValid = await _userManager.CheckPasswordAsync(managedUser, password);

        if (!isPasswordValid)
            return InvalidPassword(email, managedUser.UserName);

        var roles = await _userManager.GetRolesAsync(managedUser);
        var accessToken = _tokenService.CreateToken(managedUser, roles[0]);

        return new AuthResult(true, "", managedUser.Email, managedUser.UserName, managedUser.PhoneNumber, accessToken);
    }


    private static AuthResult FailedRegistration(IdentityResult result, string email, string username, string phoneNumber)
    {
        var authResult = new AuthResult(false, "",email, username,phoneNumber, "");

        foreach (var error in result.Errors) authResult.ErrorMessages.Add(error.Code, error.Description);

        return authResult;
    }


    private static AuthResult InvalidEmail(string email)
    {
        var result = new AuthResult(false, "",email, "", "", "");
        result.ErrorMessages.Add("Bad credentials", "Invalid email");
        return result;
    }

    private static AuthResult InvalidPassword(string email, string userName)
    {
        var result = new AuthResult(false, "",email, userName, "", "");
        result.ErrorMessages.Add("Bad credentials", "Invalid password");
        return result;
    }
}