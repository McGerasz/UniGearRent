using System.Net;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using UniGearRentAPI.Contracts;
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
        _factory._testUniGearRentAPIDbContext.Users.Add(new IdentityUser
        {
            AccessFailedCount = 0,
            EmailConfirmed = true,
            Id = "TESTID3",
            LockoutEnabled = false,
            UserName = "TestUser3"
        });
        _factory._testUniGearRentAPIDbContext.Users.Add(new IdentityUser
        {
            AccessFailedCount = 0,
            EmailConfirmed = true,
            Id = "TESTID4",
            LockoutEnabled = false,
            UserName = "TestUser4"
        });
        _factory._testUniGearRentAPIDbContext.Users.Add(new IdentityUser
        {
            AccessFailedCount = 0,
            EmailConfirmed = true,
            Id = "PUTLESSORTESTID",
            LockoutEnabled = false,
            UserName = "PUTLESSORTEST"
        });
        _factory._testUniGearRentAPIDbContext.Users.Add(new IdentityUser
        {
            AccessFailedCount = 0,
            EmailConfirmed = true,
            Id = "PUTUSERTESTID",
            LockoutEnabled = false,
            UserName = "PUTUSERTEST"
        });
        _factory._testUniGearRentAPIDbContext.LessorsDetails.Add(new LessorDetails
        {
            Name = "TESTNAME1",
            PosterId = "TESTID1"
        });
        _factory._testUniGearRentAPIDbContext.LessorsDetails.Add(new LessorDetails
        {
            Name = "TESTNAME2",
            PosterId = "TESTID2"
        });
        _factory._testUniGearRentAPIDbContext.LessorsDetails.Add(new LessorDetails
        {
            Name = "LESSORPUTTEST",
            PosterId = "PUTLESSORTESTID"
        });
        _factory._carRepository.Create(new CarPost
        {
            Id = 7,
            CanDeliverToYou = true,
            Description = "",
            Location = "Budapest",
            Name = "TESTCAR1",
            NumberOfSeats = 4,
            PosterId = "TESTID1"
        });
        _factory._trailerRepository.Create(new TrailerPost
        {
            Id = 8,
            Description = "",
            Location = "Miskolc",
            Name = "TESTTRAILER1",
            PosterId = "TESTID1",
            Width = 100,
            Length = 100
        });
        _factory._carRepository.Create(new CarPost
        {
            Id = 9,
            CanDeliverToYou = true,
            Description = "",
            Location = "Budapest",
            Name = "TESTCAR2",
            NumberOfSeats = 4,
            PosterId = "TESTID2"
        });
        _factory._testUniGearRentAPIDbContext.UsersDetails.Add(new UserDetails
        {
            FirstName = "TEST3",
            Id = "TESTID3",
            LastName = "TESTLASTNAME",
            FavouriteIDs = { (Post)_factory._carRepository.GetById(9) }
        });
        _factory._testUniGearRentAPIDbContext.UsersDetails.Add(new UserDetails
        {
            FirstName = "TEST4",
            Id = "TESTID4",
            LastName = "TESTLASTNAME",
            FavouriteIDs = { (Post) _factory._carRepository.GetById(9) }
        });
        _factory._testUniGearRentAPIDbContext.UsersDetails.Add(new UserDetails
        {
            FirstName = "TEST5",
            Id = "PUTUSERTESTID",
            LastName = "TESTLASTNAME",
            FavouriteIDs = { (Post) _factory._carRepository.GetById(9) }
        });
        _factory._testUniGearRentAPIDbContext.SaveChanges();
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
        var user1PostsGetResponse = await _client.GetAsync($"api/Post/byName/testname1");
        string responseString = await user1PostsGetResponse.Content.ReadAsStringAsync();
        var processedPosts = JsonConvert.DeserializeObject<ICollection<Dictionary<string, string>>>(responseString);
        Assert.That(processedPosts.All(x => x["posterId"] == "TESTID1"));
    }

    [Test]
    public async Task GetByLocationTest()
    {
        var budapestPostsGetResponse = await _client.GetAsync($"api/Post/byLocation/Budapest");
        string responseString = await budapestPostsGetResponse.Content.ReadAsStringAsync();
        var processedPosts = JsonConvert.DeserializeObject<ICollection<Dictionary<string, string>>>(responseString);
        Assert.That(processedPosts.All(x => x["location"] == "Budapest"));
    }

    [Test]
    public async Task GetTest()
    {
        var getResponse = await _client.GetAsync($"/api/Post/7");
        string responseString = await getResponse.Content.ReadAsStringAsync();
        var processedResponse = JsonConvert.DeserializeObject<Dictionary<string, string>>(responseString);
        Assert.That(processedResponse["id"], Is.EqualTo("7"));
    }

    [Test]
    public async Task GetLessorDetailsTest()
    {
        var response = await _client.GetAsync($"api/Post/lessorDetails/7");
        string responseString = await response.Content.ReadAsStringAsync();
        var processedResponse = JsonConvert.DeserializeObject<LessorDetails>(responseString);
        Assert.That(processedResponse.Name, Is.EqualTo("TESTNAME1"));
    }

    [Test]
    public async Task PostFavouriteTest()
    {
        var response1 = await _client.PostAsync($"api/Post/favourite?userName=TestUser3&postId=8", null);
        var response2 = await _client.GetAsync($"/api/Post/getFavourites/TestUser3");
        string responseString = await response2.Content.ReadAsStringAsync();
        var processedPosts = JsonConvert.DeserializeObject<ICollection<Dictionary<string, string>>>(responseString);
        Assert.That(processedPosts.Count, Is.EqualTo(2));
    }

    [Test]
    public async Task DelFavouriteTest()
    {
        {
            var response1 = await _client.DeleteAsync($"api/Post/favourite?userName=TestUser4&postId=9");
            var response2 = await _client.GetAsync($"/api/Post/getFavourites/TestUser4");
            string responseString = await response2.Content.ReadAsStringAsync();
            var processedPosts = JsonConvert.DeserializeObject<ICollection<Dictionary<string, string>>>(responseString);
            Assert.That(processedPosts.Count, Is.EqualTo(0));
        }
    }
    [Test]
    public async Task ProfileDetailsReturnsLessorDetails()
    {
        var response = await _client.GetAsync($"api/Post/profileDetails/{"TESTID1"}");
        string responseString = await response.Content.ReadAsStringAsync();
        var processedResponse = JsonConvert.DeserializeObject<LessorDetails>(responseString);
        Assert.That(processedResponse, Is.TypeOf<LessorDetails>());
    }
}