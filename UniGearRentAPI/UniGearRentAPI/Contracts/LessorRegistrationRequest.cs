using System.ComponentModel.DataAnnotations;

namespace UniGearRentAPI.Contracts;

public record LessorRegistrationRequest(
    [Required]string Email,
    [Required]string Username,
    [Required]string PhoneNumber,
    [Required]string Name,
    [Required]string Password);