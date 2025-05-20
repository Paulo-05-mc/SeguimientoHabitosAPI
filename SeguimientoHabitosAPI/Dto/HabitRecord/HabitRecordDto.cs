using SeguimientoHabitosAPI.Models;

namespace SeguimientoHabitosAPI.Dto.HabitRecord;

public class HabitRecordDto
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public bool IsCompleted { get; set; }
}