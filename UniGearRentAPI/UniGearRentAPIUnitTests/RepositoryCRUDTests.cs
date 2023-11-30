using Microsoft.AspNetCore.Identity;
using UniGearRentAPI.DatabaseServices;
using UniGearRentAPI.DatabaseServices.Repositories;
using UniGearRentAPI.Models;

namespace UniGearRentAPIUnitTests;

public class Tests
{
    private IRepository<CarPost> _carPostRepository;
    private IRepository<TrailerPost> _trailerPostRepository;
    private UniGearRentAPIDbContext _dbContext;
    
    [OneTimeSetUp]
    public void OneTimeSetup()
    {
        Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "Test");
        Environment.SetEnvironmentVariable("ASPNETCORE_ISSUERSIGNINGKEY", "PlaceholderSigningKey123");
        Environment.SetEnvironmentVariable("ASPNETCORE_VALIDAUDIENCE", "PlaceholderAudience");
        Environment.SetEnvironmentVariable("ASPNETCORE_VALIDISSUER", "PlaceholderIssuer");
        _dbContext = new UniGearRentAPIDbContext();
        _dbContext.Users.Add(new IdentityUser
        {
            AccessFailedCount = 0,
            EmailConfirmed = true,
            Id = "TESTID",
            LockoutEnabled = false
        });
    }
    [SetUp]
    public void Setup()
    {
        _carPostRepository = new CarPostRepository(_dbContext);
        _trailerPostRepository = new TrailerPostRepository(_dbContext);
    }

    [Test]
    public void CarPostRepositoryCRUDTest()
    {
        CarPost newCarPost = new CarPost
        {
            CanDeliverToYou = true,
            Descritption = "",
            Id = 1,
            Name = "TESTCARPOST",
            Location = "TESTLOCATION",
            NumberOfSeats = 2,
            PosterId = "TESTID"
        };
        _carPostRepository.Create(newCarPost);
        Assert.That(_carPostRepository.GetAll().Count(), Is.EqualTo(1));
        newCarPost.Descritption = "TESTDESCRIPTION";
        _carPostRepository.Update(newCarPost);
        var retrievedCarPost = _carPostRepository.GetById(1);
        Assert.That(retrievedCarPost.Descritption, Is.EqualTo("TESTDESCRIPTION"));
        _carPostRepository.Delete(1);
        Assert.That(_carPostRepository.GetAll().Count(), Is.EqualTo(0));
    }
    [Test]
    public void TrailerPostRepositoryCRUDTest()
    {
        TrailerPost newTrailerPost = new TrailerPost
        {
            Descritption = "",
            Id = 2,
            Name = "TESTTRAILERPOST",
            Location = "TESTLOCATION",
            PosterId = "TESTID",
            Width = 0,
            Length = 0
        };
        _trailerPostRepository.Create(newTrailerPost);
        Assert.That(_trailerPostRepository.GetAll().Count(), Is.EqualTo(1));
        newTrailerPost.Descritption = "TESTDESCRIPTION";
        _trailerPostRepository.Update(newTrailerPost);
        var retrievedTrailerPost = _trailerPostRepository.GetById(2);
        Assert.That(retrievedTrailerPost.Descritption, Is.EqualTo("TESTDESCRIPTION"));
        _trailerPostRepository.Delete(2);
        Assert.That(_trailerPostRepository.GetAll().Count(), Is.EqualTo(0));
    }
}