using SeguimientoHabitosAPI.Models;

namespace SeguimientoHabitosAPI.Dto.Reflexion;

public class ReflexionDto
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public string Content { get; set; }
    public int? HabitId { get; set; }
}