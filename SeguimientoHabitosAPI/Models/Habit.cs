using System.ComponentModel.DataAnnotations;
using SeguimientoHabitosAPI.Models;
public class Habit
{
    public int Id { get; set; }
    
    [Required]
    [MaxLength(100)]
    public string Name { get; set; }
    
    [MaxLength(500)]
    public string Description { get; set; }
    
    [Required]
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    
    [Required]
    public bool IsActive { get; set; } = true;
    
    [Required]
    public int UserId { get; set; }
    public User User { get; set; }
    
    public ICollection<HabitRecord> Records { get; set; }
    public ICollection<Reflexion> Reflections { get; set; }
}