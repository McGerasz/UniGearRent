using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using UniGearRentAPI.DatabaseServices;

namespace UniGearRentAPI.Services.Authentication;

public class IdService : IIdService
{
    private readonly UniGearRentAPIDbContext _dbContext;

    public IdService(UniGearRentAPIDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public string GetId(string userName)
    {
        return _dbContext.Users.ToList().First(user =>
            user.UserName == userName).Id;
    }

    public string[] GetIdsContainingName(string name)
    {
        var lessors = _dbContext.LessorsDetails.Where(detail => detail.Name.ToLower().Contains(name.ToLower()));
        string[] ids = new string[lessors.Count()];
        int index = 0;
        foreach (var lessor in lessors)
        {
            ids[index] = lessor.PosterId;
            index++;
        }
        return ids;
    }
}