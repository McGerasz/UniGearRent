using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace UniGearRentAPI.Models;

public class LessorDetails
{
    [Key]
    public string PosterId { get; set; }
    public string Name { get; set; }
    public ICollection<Post> Posts { get; set; }
}