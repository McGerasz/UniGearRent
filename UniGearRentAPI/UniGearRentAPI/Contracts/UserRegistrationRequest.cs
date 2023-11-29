using System.ComponentModel.DataAnnotations;

namespace UniGearRentAPI.Contracts;

public record UserRegistrationRequest(
    [Required]string Email,
    [Required]string Username,
    [Required]string PhoneNumber,
    [Required]string FirstName,
    [Required]string LastName,
    [Required]string Password);