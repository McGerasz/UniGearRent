using System.Net;
using System.Net.Http.Json;
using Newtonsoft.Json;
using UniGearRentAPI.Models;

namespace UniGearRentAPIIntegrationTests;

public class Tests
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
        _client = _factory.CreateClient();
    }
    [OneTimeTearDown]
    public void OneTimeTearDown()
    {
        _factory.Dispose();
        _client.Dispose();
    }
    [Test]
    public async Task CarControllerCRUDTest()
    {
        var initialCarPost = new CarPost
        {
            Id = 1,
            CanDeliverToYou = true,
            Description = "",
            Location = "",
            Name = "Initial Car Post",
            PosterId = "",
            NumberOfSeats = 4
        };
        var postResponse = await _client.PostAsync("api/Car?userName=Admin", JsonContent.Create(initialCarPost));
        var initialGetResponse = await _client.GetAsync($"api/Car/1");
        string responseString = await initialGetResponse.Content.ReadAsStringAsync();
        var carPost = JsonConvert.DeserializeObject<CarPost>(responseString);
        Assert.That(carPost.Name, Is.EqualTo("Initial Car Post"));

        var patchResponse = await _client.PatchAsync("api/Car/1?name=successfulPatch", null);
        var afterPatchGetResponse = await _client.GetAsync($"api/Car/1");
        string responseStringAfterPatch = await afterPatchGetResponse.Content.ReadAsStringAsync();
        var carPostAfterPatch = JsonConvert.DeserializeObject<CarPost>(responseStringAfterPatch);
        Assert.That(carPostAfterPatch.Name, Is.EqualTo("successfulPatch"));

        carPostAfterPatch.Name = "Successful Put Request";
        var putResponse = await _client.PutAsync("api/Car", JsonContent.Create(carPostAfterPatch));
        var afterPutGetResponse = await _client.GetAsync("api/Car/1");
        string responseStringAfterPut = await afterPutGetResponse.Content.ReadAsStringAsync();
        var carPostAfterPut = JsonConvert.DeserializeObject<CarPost>(responseStringAfterPut);
        Assert.That(carPostAfterPut.Name, Is.EqualTo("Successful Put Request"));

        var deleteResponse = await _client.DeleteAsync($"api/Car/1");
        var afterDeleteGetResponse = await _client.GetAsync("api/Car/1");
        Assert.That(afterDeleteGetResponse.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
    }
}