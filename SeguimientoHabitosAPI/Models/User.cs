using System.ComponentModel.DataAnnotations;
using SeguimientoHabitosAPI.Models;

public class User
{
    public int Id { get; set; }
    
    [Required]
    [MaxLength(100)]
    public string Name { get; set; }
    
    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required, MinLength(6)] 
    public string PasswordHash { get; set; } = null!;
    
    [Required]
    public Role Role { get; set; }
    
    // Relación para usuarios asignados a un coach
    public ICollection<User> AssignedUsers { get; set; } // Solo para coaches
    public User Coach { get; set; } // Null para admin y usuarios sin coach
    public int? CoachId { get; set; }
    
    public ICollection<Habit> Habits { get; set; }
    public ICollection<Reflexion> Reflexiones { get; set; }
}

public enum Role
{
    User = 0,
    Coach= 1,
    Admin = 2
}