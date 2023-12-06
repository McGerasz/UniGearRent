using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using UniGearRentAPI.DatabaseServices;
using UniGearRentAPI.DatabaseServices.Repositories;
using UniGearRentAPI.Models;

namespace UniGearRentAPIIntegrationTests;

public class CustomWebApplicationFactory : WebApplicationFactory<Program>
{

    public IRepository<CarPost> _carRepository;
    public IRepository<TrailerPost> _trailerRepository;
    public UniGearRentAPIDbContext _testUniGearRentAPIDbContext { get; }

    public CustomWebApplicationFactory()
    {
        _testUniGearRentAPIDbContext = new UniGearRentAPIDbContext();
        _carRepository = new CarPostRepository(_testUniGearRentAPIDbContext);
        _trailerRepository = new TrailerPostRepository(_testUniGearRentAPIDbContext);
    }
    
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        base.ConfigureWebHost(builder);
        builder.ConfigureTestServices(services =>
        {
            services.AddDbContext<UniGearRentAPIDbContext>();
            services.AddTransient<IRepository<CarPost>, CarPostRepository>();
            services.AddTransient<IRepository<TrailerPost>, TrailerPostRepository>();
            services.AddSingleton<IPolicyEvaluator, FakePolicyEvaluator>();
        });

        builder.UseEnvironment("Test");
    }
}