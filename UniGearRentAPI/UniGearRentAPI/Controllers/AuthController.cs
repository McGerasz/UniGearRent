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
    private readonly ILogger<AuthController> _logger;

    public AuthController(IAuthService authService, UniGearRentAPIDbContext dbContext, ILogger<AuthController> logger)
    {
        _authService = authService;
        _dbContext = dbContext;
        _logger = logger;
    }
    [HttpPost("RegisterUser")]
    public async Task<ActionResult<RegistrationResponse>> RegisterUser(UserRegistrationRequest request)
    {
        _logger.LogInformation("Beginning operation");
        _logger.LogInformation("Validating model state...");
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        _logger.LogInformation("Registering new user...");
        var result = await _authService.RegisterAsync(request.Email, request.Username,
            request.Password, request.PhoneNumber, "User");
        
        if (!result.Success)
        {
            AddErrors(result);
            return BadRequest(ModelState);
        }
        _logger.LogInformation("Registration successful");
        _logger.LogInformation("Updating database with new UserDetails...");
        await _dbContext.UsersDetails.AddAsync(new UserDetails
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            PosterId = result.UserId
        });
        await _dbContext.SaveChangesAsync();
        _logger.LogInformation("Operation successful");
        return CreatedAtAction(nameof(RegisterUser), new RegistrationResponse(result.Email, result.UserName, result.PhoneNumber));
    }
    [HttpPost("RegisterLessor")]
    public async Task<ActionResult<RegistrationResponse>> RegisterLessor(LessorRegistrationRequest request)
    {
        _logger.LogInformation("Beginning operation");
        _logger.LogInformation("Validating model state...");
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        _logger.LogInformation("Registering new user...");
        var result = await _authService.RegisterAsync(request.Email, request.Username, request.Password, request.PhoneNumber, 
             "Lessor");

        if (!result.Success)
        {
            AddErrors(result);
            return BadRequest(ModelState);
        }
        _logger.LogInformation("Registration successful");
        _logger.LogInformation("Updating database with new LessorDetails...");
        await _dbContext.LessorsDetails.AddAsync(new LessorDetails
        {
            Name = request.Name,
            PosterId = result.UserId
        });
        await _dbContext.SaveChangesAsync();
        _logger.LogInformation("Operation successful");
        return CreatedAtAction(nameof(RegisterUser), new RegistrationResponse(result.Email, result.UserName, result.PhoneNumber));
    }
    
    [HttpPost("Login")]
    public async Task<ActionResult<AuthResponse>> Authenticate([FromBody] AuthRequest request)
    {
        _logger.LogInformation("Beginning operation");
        _logger.LogInformation("Validating model state...");
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        _logger.LogInformation("Attempting to login...");
        var result = await _authService.LoginAsync(request.Email, request.Password);

        if (!result.Success)
        {
            _logger.LogError("Login attempt unsuccessful");
            AddErrors(result);
            return BadRequest(ModelState);
        }
        _logger.LogInformation("Operation successful");
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