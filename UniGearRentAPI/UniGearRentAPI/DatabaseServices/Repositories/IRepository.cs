namespace UniGearRentAPI.DatabaseServices.Repositories;

public interface IRepository<T> where T : class
{
    IEnumerable<T> GetAll();
    T? GetById(int id);
    void Create(T obj);
    void Update(T obj);
    void Delete(int id);
}