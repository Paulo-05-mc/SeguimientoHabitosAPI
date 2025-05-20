using System.ComponentModel.DataAnnotations;
using SeguimientoHabitosAPI.Models;

namespace SeguimientoHabitosAPI.Dto.Reflexion;

public class ReflexionCreateDto
{
    [Required]
    public DateTime Date { get; set; }

    [Required]
    [MaxLength(2000)]
    public string Content { get; set; }

    public int? HabitId { get; set; }
}