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

    public PostController(IRepository<CarPost> carRepository, IRepository<TrailerPost> trailerRepository, IIdService idService)
    {
        _carRepository = carRepository;
        _trailerRepository = trailerRepository;
        _idService = idService;
    }

    [HttpGet("byUser/{user}")]
    public IActionResult GetByUsername([FromRoute]string user)
    {
        List<Post> postList = new List<Post>();
        string userId = _idService.GetId(user);
        foreach (var carPost in _carRepository.GetAll())
        {
            if(carPost.PosterId == userId) postList.Add(carPost);
        }
        foreach (var trailerPost in _trailerRepository.GetAll())
        {
            if(trailerPost.PosterId == userId) postList.Add(trailerPost);
        }

        return Ok(postList);
    }

    [HttpGet("byLocation/{location}")]
    public IActionResult GetByLocation([FromRoute] string location)
    {
        List<Post> postList = new List<Post>();
        foreach (var carPost in _carRepository.GetAll())
        {
            if(carPost.Location == location) postList.Add(carPost);
        }
        foreach (var trailerPost in _trailerRepository.GetAll())
        {
            if(trailerPost.Location == location) postList.Add(trailerPost);
        }

        return Ok(postList);
    }
}