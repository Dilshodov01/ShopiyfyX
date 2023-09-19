using System.ComponentModel.DataAnnotations;

namespace ShopiyfyX.Service.DTOs.UserDto;

public class UserForCreationDto
{
    [Required(ErrorMessage = "Enter the FirstName")]
    public string FirstName { get; set; }

    [Required(ErrorMessage = "Enter the LastName")]
    public string LastName { get; set; }

    [EmailAddress(ErrorMessage = "Enter properly")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Enter the password")]
    public string Password { get; set; }

    [Required(ErrorMessage = "Enter the Phone number")]
    public string PhoneNumber { get; set; }
}
