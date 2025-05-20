using System.ComponentModel.DataAnnotations;
using SeguimientoHabitosAPI.Models;

namespace SeguimientoHabitosAPI.Dto.Habit;

public class HabitCreateDto
{
    [Required]
    [MaxLength(100)]
    public string Name { get; set; }

    [MaxLength(500)]
    public string Description { get; set; }
}