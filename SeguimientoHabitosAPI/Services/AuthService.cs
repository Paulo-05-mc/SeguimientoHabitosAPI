using Microsoft.EntityFrameworkCore;
using SeguimientoHabitosAPI.Data;
using SeguimientoHabitosAPI.Dto.User;
using SeguimientoHabitosAPI.Models;
using Microsoft.AspNetCore.Identity;

namespace SeguimientoHabitosAPI.Services
{
    public interface IAuthService
    {
        Task<ServiceResponse<string>> Register(UserRegisterDto request);
        Task<ServiceResponse<string>> Login(string email, string password);
        Task<User?> GetUserById(int userId);
    }

    public class AuthService : IAuthService
    {
        private readonly AppDbContext _context;
        private readonly ITokenService _tokenService;
        private readonly PasswordHasher<User> _hasher = new();

        public AuthService(AppDbContext context, ITokenService tokenService)
        {
            _context = context;
            _tokenService = tokenService;
        }

        public async Task<ServiceResponse<string>> Register(UserRegisterDto request)
        {
            var response = new ServiceResponse<string>();

            if (await _context.Users.AnyAsync(u => u.Email.ToLower() == request.Email.ToLower()))
            {
                response.Success = false;
                response.Message = "El usuario ya existe.";
                return response;
            }

            var user = new User
            {
                Email = request.Email,
                Name = request.Name,
                Role = request.Role
            };

            user.PasswordHash = _hasher.HashPassword(user, request.Password);

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            response.Data = _tokenService.GenerateToken(user);
            return response;
        }

        public async Task<ServiceResponse<string>> Login(string email, string password)
        {
            var response = new ServiceResponse<string>();
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email.ToLower() == email.ToLower());

            if (user == null || _hasher.VerifyHashedPassword(user, user.PasswordHash, password) != PasswordVerificationResult.Success)
            {
                response.Success = false;
                response.Message = "Credenciales inválidas.";
                return response;
            }

            response.Data = _tokenService.GenerateToken(user);
            return response;
        }

        public async Task<User?> GetUserById(int userId)
        {
            return await _context.Users.FindAsync(userId);
        }
    }
}
