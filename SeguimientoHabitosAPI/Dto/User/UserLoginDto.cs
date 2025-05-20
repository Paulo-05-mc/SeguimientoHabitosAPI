using System.ComponentModel.DataAnnotations;
using SeguimientoHabitosAPI.Models;

namespace SeguimientoHabitosAPI.Dto.User;

public class UserLoginDto
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    public string Password { get; set; }
}
