using UniGearRentAPI.Models;

namespace UniGearRentAPI.DatabaseServices.Repositories;

public class CarPostRepository : IRepository<CarPost>
{
    private readonly UniGearRentAPIDbContext _dbContext;

    public CarPostRepository(UniGearRentAPIDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public IEnumerable<CarPost> GetAll()
    {
        return _dbContext.CarPosts.ToList();
    }

    public CarPost? GetById(int id)
    {
        return _dbContext.CarPosts.FirstOrDefault(x => x.Id == id);
    }

    public void Create(CarPost obj)
    {
        _dbContext.CarPosts.Add(obj);
        _dbContext.SaveChanges();
    }

    public void Update(CarPost obj)
    {
        _dbContext.CarPosts.Update(obj);
        _dbContext.SaveChanges();
    }

    public void Delete(int id)
    {
        _dbContext.CarPosts.Remove(GetById(id));
        _dbContext.SaveChanges();
    }
}