using Microsoft.AspNetCore.Mvc;
using SeguimientoHabitosAPI.Data;
using SeguimientoHabitosAPI.Dto.User;
using SeguimientoHabitosAPI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;


namespace SeguimientoHabitosAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class AuthController(AppDbContext context, ITokenService tokenService) : ControllerBase
    {
        private readonly PasswordHasher<User> _hasher = new();

        [HttpPost("register")]
        public async Task<ActionResult> Register(UserRegisterDto dto)
        {
            if (await context.Users.AnyAsync(u => u.Email == dto.Email))
                return BadRequest("El correo ya está registrado");

            var user = new User
            {
                Email = dto.Email,
                Name = dto.Name,
                Role = Role.User
            };

            user.PasswordHash = _hasher.HashPassword(user, dto.Password);
            
            context.Users.Add(user);
            await context.SaveChangesAsync();

            return Ok(new { Token = tokenService.GenerateToken(user) });
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login(UserLoginDto dto)
        {
            var user = await context.Users.FirstOrDefaultAsync(u => u.Email == dto.Email);
            if (user == null) return Unauthorized();

            var result = _hasher.VerifyHashedPassword(user, user.PasswordHash, dto.Password);
            if (result != PasswordVerificationResult.Success) return Unauthorized();

            return Ok(new { Token = tokenService.GenerateToken(user) });
        }
    }
}