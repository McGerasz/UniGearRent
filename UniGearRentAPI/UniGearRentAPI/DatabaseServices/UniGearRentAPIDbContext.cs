using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using UniGearRentAPI.Models;

namespace UniGearRentAPI.DatabaseServices;

public class UniGearRentAPIDbContext : IdentityDbContext<IdentityUser, IdentityRole, string>
{
    public DbSet<User> Users { get; set; }
    public DbSet<Lessor> Lessors { get; set; }
    public DbSet<CarPost> CarPosts { get; set; }
    public DbSet<TrailerPost> TrailerPosts { get; set; }
    public UniGearRentAPIDbContext (DbContextOptions<UniGearRentAPIDbContext> options)
        : base(options)
    {
        if(Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") != "Test")Database.Migrate();
    }
    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Testing")
        {
            options.UseInMemoryDatabase("TestDatabase");
        }
        else options.UseNpgsql(
            Environment.GetEnvironmentVariable("ASPNETCORE_CONNECTIONSTRING"));
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Lessor>().HasMany(e => e.Posts)
            .WithOne(e => e.Lessor)
            .HasForeignKey(e => e.PosterId);
    }
}