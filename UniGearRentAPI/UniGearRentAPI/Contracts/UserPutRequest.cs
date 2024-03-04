namespace UniGearRentAPI.Contracts;

public class UserPutRequest
{
    public string Id { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string Phonenumber { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
}