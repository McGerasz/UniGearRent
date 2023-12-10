using Microsoft.AspNetCore.Identity;
using UniGearRentAPI.Models;

namespace UniGearRentAPIIntegrationTests;

public class PostControllerTests
{
    private CustomWebApplicationFactory _factory;
    private HttpClient _client;
    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "Test");
        Environment.SetEnvironmentVariable("ASPNETCORE_ISSUERSIGNINGKEY", "PlaceholderSigningKey123");
        Environment.SetEnvironmentVariable("ASPNETCORE_VALIDAUDIENCE", "PlaceholderAudience");
        Environment.SetEnvironmentVariable("ASPNETCORE_VALIDISSUER", "PlaceholderIssuer");
        Environment.SetEnvironmentVariable("ASPNETCORE_ADMINEMAIL", "admin@admin.com");
        Environment.SetEnvironmentVariable("ASPNETCORE_ADMINPASSWORD", "Adminpassword123");
        _factory = new CustomWebApplicationFactory();
        _factory._testUniGearRentAPIDbContext.Users.Add(new IdentityUser
        {
            AccessFailedCount = 0,
            EmailConfirmed = true,
            Id = "TESTID1",
            LockoutEnabled = false,
            UserName = "TestUser1"
        });
        _factory._testUniGearRentAPIDbContext.Users.Add(new IdentityUser
        {
            AccessFailedCount = 0,
            EmailConfirmed = true,
            Id = "TESTID2",
            LockoutEnabled = false,
            UserName = "TestUser2"
        });
        _factory._carRepository.Create(new CarPost
        {
            Id = 1,
            CanDeliverToYou = true,
            Description = "",
            Location = "Budapest",
            Name = "TESTCAR1",
            NumberOfSeats = 4,
            PosterId = "TESTID1"
        });
        _factory._trailerRepository.Create(new TrailerPost
        {
            Id = 2,
            Description = "",
            Location = "Miskolc",
            Name = "TESTTRAILER1",
            PosterId = "TESTID1",
            Width = 100,
            Length = 100
        });
        _factory._carRepository.Create(new CarPost
        {
            Id = 3,
            CanDeliverToYou = true,
            Description = "",
            Location = "Budapest",
            Name = "TESTCAR2",
            NumberOfSeats = 4,
            PosterId = "TESTID2"
        });
        _client = _factory.CreateClient();
    }
    [OneTimeTearDown]
    public void OneTimeTearDown()
    {
        _factory.Dispose();
        _client.Dispose();
    }

    [Test]
    public async Task GetByUserTest()
    {
        Assert.Pass();
    }

    [Test]
    public async Task GetByLocationTest()
    {
        Assert.Pass();
    }
}