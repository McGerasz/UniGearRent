using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor.TagHelpers;
using UniGearRentAPI.Contracts;
using UniGearRentAPI.DatabaseServices;
using UniGearRentAPI.Models;
using UniGearRentAPI.Services.Authentication;

namespace UniGearRentAPI.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly UniGearRentAPIDbContext _dbContext;

    public AuthController(IAuthService authService, UniGearRentAPIDbContext dbContext)
    {
        _authService = authService;
        _dbContext = dbContext;
    }
    [HttpPost("RegisterUser")]
    public async Task<ActionResult<RegistrationResponse>> RegisterUser(UserRegistrationRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _authService.RegisterAsync(request.Email, request.Username, request.PhoneNumber,
            request.Password, "User");

        if (!result.Success)
        {
            AddErrors(result);
            return BadRequest(ModelState);
        }
        Console.WriteLine(result.UserId);
        await _dbContext.UsersDetails.AddAsync(new UserDetails
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            PosterId = result.UserId
        });
        await _dbContext.SaveChangesAsync();
        return CreatedAtAction(nameof(RegisterUser), new RegistrationResponse(result.Email, result.UserName, result.PhoneNumber));
    }
    [HttpPost("RegisterLessor")]
    public async Task<ActionResult<RegistrationResponse>> RegisterLessor(LessorRegistrationRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _authService.RegisterAsync(request.Email, request.Username, request.PhoneNumber,
            request.Password, "Lessor");

        if (!result.Success)
        {
            AddErrors(result);
            return BadRequest(ModelState);
        }

        await _dbContext.LessorsDetails.AddAsync(new LessorDetails
        {
            Name = request.Name,
            PosterId = result.UserId
        });
        await _dbContext.SaveChangesAsync();
        return CreatedAtAction(nameof(RegisterUser), new RegistrationResponse(result.Email, result.UserName, result.PhoneNumber));
    }
    
    [HttpPost("Login")]
    public async Task<ActionResult<AuthResponse>> Authenticate([FromBody] AuthRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _authService.LoginAsync(request.Email, request.Password);

        if (!result.Success)
        {
            AddErrors(result);
            return BadRequest(ModelState);
        }

        return Ok(new AuthResponse(result.Email, result.UserName, result.PhoneNumber,result.Token));
    }
    
    private void AddErrors(AuthResult result)
    {
        foreach (var error in result.ErrorMessages)
        {
            ModelState.AddModelError(error.Key, error.Value);
        }
    }
}