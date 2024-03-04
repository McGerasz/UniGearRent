using UniGearRentAPI.Models;

namespace UniGearRentAPI.Contracts;

public class LessorPutRequest
{
    public string Id { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string Phonenumber { get; set; }
    public string Name { get; set; }
}