using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace UniGearRentAPI.Models;

public abstract class Post
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string Name { get; set; }
    public string Location { get; set; }
    public string PosterId { get; set; }
    public Lessor? Lessor { get; set; }
    public string Descritption { get; set; }
    public int? HourlyPrice { get; set; }
    public int? DailyPrice { get; set; }
    public int? WeeklyPrice { get; set; }
    public int? SecurityDeposit { get; set; }
}