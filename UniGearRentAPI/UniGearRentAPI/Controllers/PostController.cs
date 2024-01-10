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

    [HttpGet("byName/{name}")]
    public IActionResult GetByUsername([FromRoute]string name)
    {
        _logger.LogInformation("Beginning operation");
        List<Post> postList = new List<Post>();
        _logger.LogInformation("Retrieving ids of matching users...");
        var ids = _idService.GetIdsContainingName(name);
        _logger.LogInformation("Ids retrieved");
        foreach (var id in ids)
        {
            foreach (var carPost in _carRepository.GetAll())
            {
                if(carPost.PosterId == id)
                {
                    carPost.LessorDetails = null;
                    postList.Add(carPost);
                }
            }
            foreach (var trailerPost in _trailerRepository.GetAll())
            {
                if(trailerPost.PosterId == id)
                {
                    trailerPost.LessorDetails = null;
                    postList.Add(trailerPost);
                }
            }
        }

        foreach (var post in postList)
        {
            _logger.LogInformation(post.Name);
        }
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
            if(carPost.Location.Contains(location)) postList.Add(carPost);
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