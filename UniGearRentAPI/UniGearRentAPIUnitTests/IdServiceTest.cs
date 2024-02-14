using Microsoft.AspNetCore.Identity;
using UniGearRentAPI.DatabaseServices;
using UniGearRentAPI.Models;
using UniGearRentAPI.Services.Authentication;

namespace UniGearRentAPIUnitTests;

public class IdServiceTest
{
    private UniGearRentAPIDbContext _dbContext;
    private IIdService _idService;

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "Test");
        Environment.SetEnvironmentVariable("ASPNETCORE_ISSUERSIGNINGKEY", "PlaceholderSigningKey123");
        Environment.SetEnvironmentVariable("ASPNETCORE_VALIDAUDIENCE", "PlaceholderAudience");
        Environment.SetEnvironmentVariable("ASPNETCORE_VALIDISSUER", "PlaceholderIssuer");
        Environment.SetEnvironmentVariable("ASPNETCORE_ADMINEMAIL", "admin@admin.com");
        Environment.SetEnvironmentVariable("ASPNETCORE_ADMINPASSWORD", "Adminpassword123");
        _dbContext = new UniGearRentAPIDbContext();
        _idService = new IdService(_dbContext);
        _dbContext.Users.Add(new IdentityUser
        {
            AccessFailedCount = 0,
            EmailConfirmed = true,
            Id = "IDSERVICETESTID",
            LockoutEnabled = false,
            UserName = "TESTUSERNAME"
        });
        _dbContext.Users.Add(new IdentityUser
        {
            AccessFailedCount = 0,
            EmailConfirmed = true,
            Id = "IDSERVICETESTID2",
            LockoutEnabled = false,
            UserName = "TESTUSERNAME2"
        });
        _dbContext.Users.Add(new IdentityUser
        {
            AccessFailedCount = 0,
            EmailConfirmed = true,
            Id = "IDSERVICETESTID3",
            LockoutEnabled = false,
            UserName = "TESTUSERNAME3"
        });
        _dbContext.LessorsDetails.Add(new LessorDetails
        {
            Name = "TEST1",
            PosterId = "IDSERVICETEST1",
        });
        _dbContext.LessorsDetails.Add(new LessorDetails
        {
            Name = "TEST2",
            PosterId = "IDSERVICETEST2",
        });
        _dbContext.LessorsDetails.Add(new LessorDetails
        {
            Name = "SOMETHING3",
            PosterId = "IDSERVICETEST3",
        });
        _dbContext.SaveChanges();
    }

    [Test]
    public void IdServiceReturnsTheRightUser()
    {
        string retrievedId = _idService.GetId("TESTUSERNAME");
        Assert.That(retrievedId, Is.EqualTo("IDSERVICETESTID"));
    }
    [Test]
    public void GetIdsContainingNameReturnsTheRightUsers()
    {
        string[] retrievedIds = _idService.GetIdsContainingName("test");
        Assert.That(retrievedIds.Length, Is.EqualTo(2));
    }
}