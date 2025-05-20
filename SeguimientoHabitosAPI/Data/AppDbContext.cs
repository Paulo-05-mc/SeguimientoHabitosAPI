using Microsoft.EntityFrameworkCore;
using SeguimientoHabitosAPI.Models;

namespace SeguimientoHabitosAPI.Data;

// Data/AppDbContext.cs
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    
    public DbSet<User> Users { get; set; }
    public DbSet<Habit> Habits { get; set; }
    public DbSet<HabitRecord> HabitRecords { get; set; }
    public DbSet<Reflexion> Reflexiones { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configuración de relaciones
        modelBuilder.Entity<User>()
            .HasMany(u => u.AssignedUsers)
            .WithOne(u => u.Coach)
            .HasForeignKey(u => u.CoachId)
            .OnDelete(DeleteBehavior.Restrict);
            
        modelBuilder.Entity<Habit>()
            .HasMany(h => h.Records)
            .WithOne(r => r.Habit)
            .OnDelete(DeleteBehavior.Cascade);
            
        modelBuilder.Entity<Habit>()
            .HasMany(h => h.Reflections)
            .WithOne(r => r.Habit)
            .OnDelete(DeleteBehavior.Cascade);
            
        modelBuilder.Entity<User>()
            .HasMany(u => u.Habits)
            .WithOne(h => h.User)
            .OnDelete(DeleteBehavior.Cascade);
            
        modelBuilder.Entity<User>()
            .HasMany(u => u.Reflexiones)
            .WithOne(r => r.User)
            .OnDelete(DeleteBehavior.Cascade);
    }
}