using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SeguimientoHabitosAPI.Models;

public class Reflexion
{
    public int Id { get; set; }
    
    [Required]
    public DateTime Date { get; set; } = DateTime.UtcNow;
    
    [Required]
    [MaxLength(2000)]
    public string Content { get; set; }
    
    public int? HabitId { get; set; }
    public Habit Habit { get; set; }
    
    [Required]
    public int UserId { get; set; }
    public User User { get; set; }
}