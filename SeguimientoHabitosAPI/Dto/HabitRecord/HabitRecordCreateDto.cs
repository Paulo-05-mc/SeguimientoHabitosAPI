using System.ComponentModel.DataAnnotations;
using SeguimientoHabitosAPI.Models;

namespace SeguimientoHabitosAPI.Dto.HabitRecord;

public class HabitRecordCreateDto
{
    [Required]
    public DateTime Date { get; set; }

    [Required]
    public bool IsCompleted { get; set; }
}