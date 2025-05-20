using System.ComponentModel.DataAnnotations;
using SeguimientoHabitosAPI.Models;

public class HabitRecord
{
    public int Id { get; set; }
    
    [Required]
    public DateTime Date { get; set; }
    
    [Required]
    public bool IsCompleted { get; set; }
    
    public int HabitId { get; set; }
    public Habit Habit { get; set; }
}