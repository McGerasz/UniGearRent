namespace UniGearRentAPI.Services.Authentication;

public interface IIdService
{
    public string GetId(string userName, string email, string phoneNumber);
}