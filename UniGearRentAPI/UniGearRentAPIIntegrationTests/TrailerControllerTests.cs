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
        
    }
}