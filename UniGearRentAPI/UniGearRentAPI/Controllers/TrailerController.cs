using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using UniGearRentAPI.DatabaseServices.Repositories;
using UniGearRentAPI.Models;
using UniGearRentAPI.Services.Authentication;

namespace UniGearRentAPI.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class TrailerController : ControllerBase
{
    private readonly IRepository<TrailerPost> _trailerRepository;
    private readonly ILogger _logger;
    private readonly IIdService _idService;

    public TrailerController(IRepository<TrailerPost> trailerRepository, ILogger<TrailerController> logger, IIdService idService)
    {
        _trailerRepository = trailerRepository;
        _logger = logger;
        _idService = idService;
    }
     [HttpGet("all")]
    public IActionResult GetAll()
    {
        _logger.LogInformation("Beginning operation");
        _logger.LogInformation("Retrieving TrailerPosts from repository...");
        var trailerPosts = _trailerRepository.GetAll();
        if(!trailerPosts.Any())
        {
            _logger.LogWarning("No TrailerPosts were found");
            return NotFound("The repository is empty");
        }
        _logger.LogInformation("Operation successful");
        return Ok(trailerPosts);
    }

    [HttpGet("{id}")]
    public IActionResult Get(int id)
    {
        _logger.LogInformation("Beginning operation");
        _logger.LogInformation("Retrieving post...");
        var trailerPost = _trailerRepository.GetById(id);
        if (trailerPost is null) return NotFound($"No post with the id {id} was found");
        _logger.LogInformation("Operation successful");
        return Ok(trailerPost);
    }

    [HttpPost]
    public IActionResult Post([FromBody] TrailerPost trailerPost, [Required]string userName)
    {
        _logger.LogInformation("Beginning operation");
        _logger.LogInformation("Updating database");
        trailerPost.PosterId = _idService.GetId(userName);
        _trailerRepository.Create(trailerPost);
        _logger.LogInformation("Operation successful");
        return Ok(_trailerRepository.GetById(trailerPost.Id));
    }

    [HttpDelete("{id:int}")]
    public IActionResult Delete(int id)
    {
        _logger.LogInformation("Beginning operation");
        _logger.LogInformation("Deleting entity...");
        _trailerRepository.Delete(id);
        return Ok();
    }

    [HttpPatch("{id:int}")]
    public IActionResult Patch([Required] int id, string? name, string? location,
        string? posterId, string? description, int? hourlyPrice, 
        int? dailyPrice, int? weeklyPrice, int? securityDeposit,
        int? width, int? length)
    {
        _logger.LogInformation("Beginning operation");
        _logger.LogInformation("Retrieving post from database...");
        var retrievedPost = _trailerRepository.GetById(id);
        if (retrievedPost is null) return NotFound("The id you wanted to update does not correspond to any post's id");
        _logger.LogInformation("Retrieved post entity from database");
        _logger.LogInformation("Updating properties...");
        retrievedPost.Id = id;
        retrievedPost.Name = name ?? retrievedPost.Name;
        retrievedPost.Location = location ?? retrievedPost.Location;
        retrievedPost.PosterId = posterId ?? retrievedPost.PosterId;
        retrievedPost.Description = description ?? retrievedPost.Description;
        retrievedPost.HourlyPrice = hourlyPrice ?? retrievedPost.HourlyPrice;
        retrievedPost.DailyPrice = dailyPrice ?? retrievedPost.DailyPrice;
        retrievedPost.WeeklyPrice = weeklyPrice ?? retrievedPost.WeeklyPrice;
        retrievedPost.SecurityDeposit = securityDeposit ?? retrievedPost.SecurityDeposit;
        retrievedPost.Width = width ?? retrievedPost.Width;
        retrievedPost.Length = length ?? retrievedPost.Length;
        _logger.LogInformation("Updating database...");
        _trailerRepository.Update(retrievedPost);
        _logger.LogInformation("Operation successful");
        return Ok();

    }

    [HttpPut]
    public IActionResult Put([FromBody] TrailerPost post)
    {
        _logger.LogInformation("Beginning operation");
        _logger.LogInformation("Updating database...");
        try
        {
            _trailerRepository.Update(post);
        }
        catch
        {
            _logger.LogError("There was an error during operation");
            _logger.LogError("It is possible that the provided id doesn't exist");
            return BadRequest("There was an error during operation");
        }
        _logger.LogInformation("Operation successful");
        return Ok();
    }
}