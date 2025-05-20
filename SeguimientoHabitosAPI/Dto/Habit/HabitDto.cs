using SeguimientoHabitosAPI.Models;

namespace SeguimientoHabitosAPI.Dto.Habit;

public class HabitDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime CreatedDate { get; set; }
    public bool IsActive { get; set; }
}