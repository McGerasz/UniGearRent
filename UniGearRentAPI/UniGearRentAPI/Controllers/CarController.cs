using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using UniGearRentAPI.DatabaseServices.Repositories;
using UniGearRentAPI.Models;

namespace UniGearRentAPI.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class CarController : ControllerBase
{
    private readonly IRepository<CarPost> _carPostRepository;
    private readonly ILogger _logger;

    public CarController(IRepository<CarPost> carPostRepository, ILogger<CarController> logger)
    {
        _carPostRepository = carPostRepository;
        _logger = logger;
    }

    [HttpGet("/all")]
    public IActionResult GetAll()
    {
        _logger.LogInformation("Beginning operation");
        _logger.LogInformation("Retrieving CarPosts from repository...");
        var carPosts = _carPostRepository.GetAll();
        if(!carPosts.Any())
        {
            _logger.LogWarning("No CarPosts were found");
            return NotFound("The repository is empty");
        }
        _logger.LogInformation("Operation successful");
        return Ok(carPosts);
    }

    [HttpGet("/{id}")]
    public IActionResult Get(int id)
    {
        _logger.LogInformation("Beginning operation");
        _logger.LogInformation("Retrieving post...");
        var carPost = _carPostRepository.GetById(id);
        if (carPost is null) return NotFound($"No post with the id {id} was found");
        _logger.LogInformation("Operation successful");
        return Ok(carPost);
    }

    [HttpPost]
    public IActionResult Post([FromBody] CarPost carPost)
    {
        _logger.LogInformation("Beginning operation");
        _logger.LogInformation("Updating database");
        _carPostRepository.Create(carPost);
        _logger.LogInformation("Operation successful");
        return Ok(_carPostRepository.GetById(carPost.Id));
    }

    [HttpDelete("/{id:int}")]
    public IActionResult Delete(int id)
    {
        _logger.LogInformation("Beginning operation");
        _logger.LogInformation("Deleting entity...");
        _carPostRepository.Delete(id);
        return Ok();
    }

    [HttpPatch("/{id:int}")]
    public IActionResult Patch([Required] int id, string? name, string? location,
        string? posterId, string? description, int? hourlyPrice, 
        int? dailyPrice, int? weeklyPrice, int? securityDeposit,
        int? numberOfSeats, bool? canDeliverToYou)
    {
        _logger.LogInformation("Beginning operation");
        _logger.LogInformation("Retrieving post from database...");
        var retrievedPost = _carPostRepository.GetById(id);
        if (retrievedPost is null) return NotFound("The id you wanted to update does not correspond to any post's id");
        _logger.LogInformation("Retrieved post entity from database");
        _logger.LogInformation("Updating properties...");
        CarPost newPost = new CarPost
        {
            Id = id,
            Name = name ?? retrievedPost.Name,
            Location = location ?? retrievedPost.Location,
            PosterId = posterId ?? retrievedPost.PosterId,
            Descritption = description ?? retrievedPost.Descritption,
            HourlyPrice = hourlyPrice ?? retrievedPost.HourlyPrice,
            DailyPrice = dailyPrice ?? retrievedPost.DailyPrice,
            WeeklyPrice = weeklyPrice ?? retrievedPost.WeeklyPrice,
            SecurityDeposit = securityDeposit ?? retrievedPost.SecurityDeposit,
            NumberOfSeats = numberOfSeats ?? retrievedPost.NumberOfSeats,
            CanDeliverToYou = canDeliverToYou ?? retrievedPost.CanDeliverToYou
        };
        _logger.LogInformation("Updating database...");
        _carPostRepository.Update(newPost);
        _logger.LogInformation("Operation successful");
        return Ok();

    }

    [HttpPut]
    public IActionResult Put([FromBody] CarPost post)
    {
        _logger.LogInformation("Beginning operation");
        _logger.LogInformation("Updating database...");
        try
        {
            _carPostRepository.Update(post);
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