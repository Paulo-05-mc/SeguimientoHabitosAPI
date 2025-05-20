using Microsoft.AspNetCore.Mvc;
using SeguimientoHabitosAPI.Data;
using SeguimientoHabitosAPI.Dto.Habit;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace SeguimientoHabitosAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class HabitsController(AppDbContext context) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<List<Habit>>> GetHabits()
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            return await context.Habits
                .Where(h => h.UserId == userId)
                .ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<Habit>> CreateHabit(HabitCreateDto dto)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            
            var habit = new Habit
            {
                Name = dto.Name,
                Description = dto.Description,
                UserId = userId
            };

            context.Habits.Add(habit);
            await context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetHabits), new { id = habit.Id }, habit);
        }
    }
}