using SeguimientoHabitosAPI.Models;

namespace SeguimientoHabitosAPI.Dto.User;

public class UserInfoDto
{
    public int Id { get; set; }
    public string Email { get; set; } = null!;
    public string Name { get; set; }
    public Role Role { get; set; }
}