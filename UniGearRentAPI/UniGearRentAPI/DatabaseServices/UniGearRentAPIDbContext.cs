using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using UniGearRentAPI.Models;

namespace UniGearRentAPI.DatabaseServices;

public class UniGearRentAPIDbContext : IdentityDbContext<IdentityUser, IdentityRole, string>
{
    public DbSet<IdentityUser> Users { get; set; }
    public DbSet<UserDetails> UsersDetails { get; set; }
    public DbSet<LessorDetails> LessorsDetails { get; set; }
    public DbSet<CarPost> CarPosts { get; set; }
    public DbSet<TrailerPost> TrailerPosts { get; set; }
    public UniGearRentAPIDbContext ()
    {
        if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") != "Test") Database.Migrate();
        else Database.EnsureCreated();
    }
    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Test")
        {
            options.UseInMemoryDatabase("TestDatabase");
        }
        else
            options.UseNpgsql(
                Environment.GetEnvironmentVariable("ASPNETCORE_CONNECTIONSTRING"));
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.Entity<LessorDetails>().HasMany(e => e.Posts)
            .WithOne(e => e.LessorDetails)
            .HasForeignKey(e => e.PosterId);
    }
}