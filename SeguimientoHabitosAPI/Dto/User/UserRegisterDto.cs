using System.ComponentModel.DataAnnotations;
using SeguimientoHabitosAPI.Models;

namespace SeguimientoHabitosAPI.Dto.User;

public class UserRegisterDto
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    [StringLength(100, MinimumLength = 6)]
    public string Password { get; set; }

    [Required]
    [MaxLength(100)]
    public string Name { get; set; }
    
    [Required]
    public Role Role { get; set; } = Role.User;
}