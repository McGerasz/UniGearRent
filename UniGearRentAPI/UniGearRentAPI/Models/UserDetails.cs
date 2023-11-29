using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace UniGearRentAPI.Models;

public class UserDetails
{
    [Key]
    public string PosterId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
}