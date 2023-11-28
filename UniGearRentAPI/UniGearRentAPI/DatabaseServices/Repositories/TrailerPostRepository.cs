using UniGearRentAPI.Models;

namespace UniGearRentAPI.DatabaseServices.Repositories;

public class TrailerPostRepository : IRepository<TrailerPost>
{
    private readonly UniGearRentAPIDbContext _dbContext;

    public TrailerPostRepository(UniGearRentAPIDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public IEnumerable<TrailerPost> GetAll()
    {
        return _dbContext.TrailerPosts.ToList();
    }

    public TrailerPost? GetById(int id)
    {
        return _dbContext.TrailerPosts.FirstOrDefault(x => x.Id == id);
    }

    public void Create(TrailerPost obj)
    {
        _dbContext.TrailerPosts.Add(obj);
        _dbContext.SaveChanges();
    }

    public void Update(TrailerPost obj)
    {
        _dbContext.TrailerPosts.Update(obj);
        _dbContext.SaveChanges();
    }

    public void Delete(int id)
    {
        _dbContext.TrailerPosts.Remove(GetById(id));
        _dbContext.SaveChanges();
    }
}