using Microsoft.AspNetCore.Identity;

namespace UniGearRentAPI.Models;

public class Lessor : IdentityUser
{
    public ICollection<Post> Posts { get; set; }
}