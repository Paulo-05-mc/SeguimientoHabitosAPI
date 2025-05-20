using Microsoft.AspNetCore.Mvc;
using SeguimientoHabitosAPI.Data;
using SeguimientoHabitosAPI.Dto.User;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace SeguimientoHabitosAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsersController(AppDbContext context) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<UserInfoDto>> GetMyProfile()
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            
            var user = await context.Users
                .Where(u => u.Id == userId)
                .Select(u => new UserInfoDto
                {
                    Id = u.Id,
                    Email = u.Email,
                    Name = u.Name,
                    Role = u.Role
                })
                .FirstOrDefaultAsync();
            
            if (user == null) 
                return NotFound();

            return user ;
        }
    }
}