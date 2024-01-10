namespace UniGearRentAPI.Services.Authentication;

public interface IIdService
{
    public string GetId(string userName);
    public string[] GetIdsContainingName(string name);
}