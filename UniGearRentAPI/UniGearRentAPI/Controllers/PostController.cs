using System.Collections;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UniGearRentAPI.DatabaseServices;
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
    private readonly UniGearRentAPIDbContext _dbContext;

    public PostController(IRepository<CarPost> carRepository, IRepository<TrailerPost> trailerRepository,
        IIdService idService, ILogger<PostController> logger, UniGearRentAPIDbContext dbContext)
    {
        _carRepository = carRepository;
        _trailerRepository = trailerRepository;
        _idService = idService;
        _logger = logger;
        _dbContext = dbContext;
    }

    [HttpGet("byName/{name}")]
    public IActionResult GetByName([FromRoute]string name)
    {
        _logger.LogInformation("Beginning operation");
        List<object> postList = new List<object>();
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
        return Ok(postList);
    }

    [HttpGet("byLocation/{location}")]
    public IActionResult GetByLocation([FromRoute] string location)
    {
        _logger.LogInformation("Beginning operation");
        List<object> postList = new List<object>();
        _logger.LogInformation("Retrieving car posts on the specified location...");
        foreach (var carPost in _carRepository.GetAll())
        {
            if(carPost.Location.ToLower().Contains(location.ToLower())) postList.Add(carPost);
        }
        _logger.LogInformation("Retrieving trailer posts on the specified location...");
        foreach (var trailerPost in _trailerRepository.GetAll())
        {
            if(trailerPost.Location.ToLower().Contains(location.ToLower())) postList.Add(trailerPost);
        }
        _logger.LogInformation("Operation successful");
        return Ok(postList);
    }
    [HttpGet("byUser/{user}")]
    public IActionResult GetByUsername([FromRoute]string user)
    {
        _logger.LogInformation("Beginning operation");
        List<object> postList = new List<object>();
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
    [HttpGet("{id}")]
    public IActionResult GetPost([FromRoute] int id)
    {
        _logger.LogInformation("Beginning operation");
        _logger.LogInformation("Searching for post in car repository...");
        object? post = _carRepository.GetAll().FirstOrDefault(obj => obj.Id == id);
        if (post is null)
        {
            _logger.LogInformation("Post was not found in car repository");
            _logger.LogInformation("Searching for post in trailer repository...");
            post = _trailerRepository.GetAll().FirstOrDefault(obj => obj.Id == id);
        }

        if (post is null)
        {
            _logger.LogInformation("The post was not found in the database");
            return NotFound("No post with the provided id was found");
        }

        _logger.LogInformation("Operation successful");
        return Ok(post);
    }

    [HttpGet("lessorDetails/{id}")]
    public IActionResult getLessorDetails([Required] int id)
    {
        _logger.LogInformation("Beginning operation");
        _logger.LogInformation("Searching for post in car repository...");
        Post? post = _carRepository.GetAll().FirstOrDefault(obj => obj.Id == id);
        if (post is null)
        {
            _logger.LogInformation("Post was not found in car repository");
            _logger.LogInformation("Searching for post in trailer repository...");
            post = _trailerRepository.GetAll().FirstOrDefault(obj => obj.Id == id);
        }

        if (post is null)
        {
            _logger.LogInformation("The post was not found in the database");
            return NotFound("No post with the provided id was found");
        }

        LessorDetails details =
            _dbContext.LessorsDetails.First(lessorDetails => lessorDetails.PosterId == post.PosterId);
        details.Posts = null;
        _logger.LogInformation("Operation successful");
        return Ok(details);
    }

    [HttpPost("favourite")]
    public IActionResult PostFavourite([Required] string userName, [Required] int postId)
    {
        _logger.LogInformation("Beginning operation");
        _logger.LogInformation("Retrieving user...");
        string id;
        try
        {
            id = _idService.GetId(userName);
        }
        catch (Exception e)
        {
            _logger.LogWarning("The user was not found in the database");
            return NotFound("The username was not found");
        }
        _logger.LogInformation("Retrieving user details...");
        var details = _dbContext.UsersDetails.Include(det => det.FavouriteIDs).First(det => det.Id == id);
        _logger.LogInformation("Retrieving post from the database...");
        Post? post = (Post)_carRepository.GetById(postId) ?? (Post)_trailerRepository.GetById(postId);
        if (post is null) return NotFound("The provided post id was not found in the database");
        _logger.LogInformation("Updating database...");
        details.FavouriteIDs.Add(post);
        _dbContext.Update(details);
        _dbContext.SaveChanges();
        _logger.LogInformation("Operation successful");
        return Ok("OK");
    }
    [HttpGet("getFavourites/{userName}")]
    public IActionResult GetFavourites(string userName)
    {
        var id = _idService.GetId(userName);
        var details = _dbContext.UsersDetails.Include(det => det.FavouriteIDs).First(det => det.Id == id);
        return Ok(details.FavouriteIDs);
    }
    [HttpDelete("delFavourites")]
    public IActionResult DelFavourites([Required]string userName, [Required]int postId)
    {
        var id = _idService.GetId(userName);
        var details = _dbContext.UsersDetails.Include(det => det.FavouriteIDs).First(det => det.Id == id);
        Post? post = (Post)(_carRepository.GetById(postId) ?? (Post)_trailerRepository.GetById(postId));
        details.FavouriteIDs.Remove(post);
        _dbContext.Update(details);
        _dbContext.SaveChanges();
        return Ok("Deleted");
    }
}