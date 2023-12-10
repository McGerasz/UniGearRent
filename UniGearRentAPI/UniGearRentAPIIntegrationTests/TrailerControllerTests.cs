using System.Net;
using System.Net.Http.Json;
using Newtonsoft.Json;
using UniGearRentAPI.Models;

namespace UniGearRentAPIIntegrationTests;

public class TrailerControllerTests
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
    public async Task TrailerControllerCRUDTest()
    {
        var initialTrailerPost = new TrailerPost
        {
            Id = 1,
            Description = "",
            Location = "",
            Name = "Initial Trailer Post",
            PosterId = "",
            Width = 100,
            Length = 100
        };
        var postResponse = await _client.PostAsync("api/Trailer?userName=Admin", JsonContent.Create(initialTrailerPost));
        var initialGetResponse = await _client.GetAsync($"api/Trailer/1");
        string responseString = await initialGetResponse.Content.ReadAsStringAsync();
        var trailerPost = JsonConvert.DeserializeObject<TrailerPost>(responseString);
        Assert.That(trailerPost.Name, Is.EqualTo("Initial Trailer Post"));

        var patchResponse = await _client.PatchAsync("api/Trailer/1?name=successfulPatch", null);
        var afterPatchGetResponse = await _client.GetAsync($"api/Trailer/1");
        string responseStringAfterPatch = await afterPatchGetResponse.Content.ReadAsStringAsync();
        var trailerPostAfterPatch = JsonConvert.DeserializeObject<TrailerPost>(responseStringAfterPatch);
        Assert.That(trailerPostAfterPatch.Name, Is.EqualTo("successfulPatch"));

        trailerPostAfterPatch.Name = "Successful Put Request";
        var putResponse = await _client.PutAsync("api/Trailer", JsonContent.Create(trailerPostAfterPatch));
        var afterPutGetResponse = await _client.GetAsync("api/Trailer/1");
        string responseStringAfterPut = await afterPutGetResponse.Content.ReadAsStringAsync();
        var trailerPostAfterPut = JsonConvert.DeserializeObject<TrailerPost>(responseStringAfterPut);
        Assert.That(trailerPostAfterPut.Name, Is.EqualTo("Successful Put Request"));

        var deleteResponse = await _client.DeleteAsync($"api/Trailer/1");
        var afterDeleteGetResponse = await _client.GetAsync("api/Trailer/1");
        Assert.That(afterDeleteGetResponse.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
    }
    
}