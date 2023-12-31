using System.Collections;
using Microsoft.AspNetCore.Mvc;
using UniGearRentAPI.DatabaseServices.Repositories;
using UniGearRentAPI.Models;
using UniGearRentAPI.Services.Authentication;

namespace UniGearRentAPI.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class PostController : ControllerBase
{
    private readonly IRepository<CarPost> _carRepository;
    private readonly IRepository<TrailerPost> _trailerRepository;
    private readonly IIdService _idService;
    private readonly ILogger<PostController> _logger;

    public PostController(IRepository<CarPost> carRepository, IRepository<TrailerPost> trailerRepository,
        IIdService idService, ILogger<PostController> logger)
    {
        _carRepository = carRepository;
        _trailerRepository = trailerRepository;
        _idService = idService;
        _logger = logger;
    }

    [HttpGet("byUser/{user}")]
    public IActionResult GetByUsername([FromRoute]string user)
    {
        _logger.LogInformation("Beginning operation");
        List<Post> postList = new List<Post>();
        string userId = "";
        try
        {
            _logger.LogInformation("Retrieving user id...");
            userId = _idService.GetId(user);
        }
        catch
        {
            _logger.LogError("User was not found in the database");
            return BadRequest($"No user with the username: {user} was found");
        }
        _logger.LogInformation("Retrieving the user's car posts...");
        foreach (var carPost in _carRepository.GetAll())
        {
            if(carPost.PosterId == userId) postList.Add(carPost);
        }
        _logger.LogInformation("Retrieving user's trailer posts...");
        foreach (var trailerPost in _trailerRepository.GetAll())
        {
            if(trailerPost.PosterId == userId) postList.Add(trailerPost);
        }
        _logger.LogInformation("Operation successful");
        return Ok(postList);
    }

    [HttpGet("byLocation/{location}")]
    public IActionResult GetByLocation([FromRoute] string location)
    {
        _logger.LogInformation("Beginning operation");
        List<Post> postList = new List<Post>();
        _logger.LogInformation("Retrieving car posts on the specified location...");
        foreach (var carPost in _carRepository.GetAll())
        {
            if(carPost.Location == location) postList.Add(carPost);
        }
        _logger.LogInformation("Retrieving trailer posts on the specified location...");
        foreach (var trailerPost in _trailerRepository.GetAll())
        {
            if(trailerPost.Location == location) postList.Add(trailerPost);
        }
        _logger.LogInformation("Operation successful");
        return Ok(postList);
    }
}