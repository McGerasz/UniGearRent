using Microsoft.AspNetCore.Identity;
using UniGearRentAPI.DatabaseServices;
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
        _dbContext.SaveChanges();
    }

    [Test]
    public void IdServiceReturnsTheRightUser()
    {
        string retrievedId = _idService.GetId("TESTUSERNAME");
        Assert.That(retrievedId, Is.EqualTo("IDSERVICETESTID"));
    }

    
}