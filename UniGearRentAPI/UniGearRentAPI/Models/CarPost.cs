using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniGearRentAPI.Models;

public class CarPost : Post
{
    public int NumberOfSeats { get; set; }
    public bool CanDeliverToYou { get; set; }
}