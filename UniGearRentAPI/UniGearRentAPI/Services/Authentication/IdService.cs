using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace UniGearRentAPI.Services.Authentication;

public class IdService : IIdService
{
    private readonly IdentityDbContext<IdentityUser, IdentityRole, string> _dbContext;

    public IdService(IdentityDbContext<IdentityUser, IdentityRole, string> dbContext)
    {
        _dbContext = dbContext;
    }
    public string GetId(string userName)
    {
        return _dbContext.Users.ToList().First(user =>
            user.UserName == userName).Id;
    }
}