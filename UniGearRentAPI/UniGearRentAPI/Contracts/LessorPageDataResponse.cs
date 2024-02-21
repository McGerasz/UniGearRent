using UniGearRentAPI.Models;

namespace UniGearRentAPI.Contracts;

public class LessorPageDataResponse
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public ICollection<Post> Posts { get; set; }
}