using Microsoft.AspNetCore.Mvc;
using SeguimientoHabitosAPI.Data;
using SeguimientoHabitosAPI.Dto.Reflexion;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using SeguimientoHabitosAPI.Models;

namespace SeguimientoHabitosAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ReflectionsController(AppDbContext context) : ControllerBase
    {
        [HttpPost]
        public async Task<ActionResult> AddReflection(ReflexionCreateDto dto)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            
            var reflexion = new Reflexion
            {
                Content = dto.Content,
                Date = DateTime.UtcNow,
                HabitId = dto.HabitId,
                UserId = userId
            };

            context.Reflexiones.Add(reflexion);
            await context.SaveChangesAsync();

            return Ok(reflexion);
        }
    }
}