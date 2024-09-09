using LivroMente.Data.Context;
using LivroMente.Domain.Models.IdentityEntities;
using LivroMente.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;
using System.Text;
using LivroMente.Domain.ViewModels;
using LivroMente.Domain.Requests;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace LivroMente.Service.Services
{
    public class UserService : BaseService<User>, IUserService<User>
    {
        private readonly DataContext _context;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly IConfiguration _configuration;
        public UserService(DataContext context, UserManager<User> userManager, 
            RoleManager<Role> roleManager, IConfiguration configuration): base(context)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
        }
        public List<UserRole> GetUserRolesInclude()
        {
             IQueryable<UserRole> entity =  _context.UserRoles
                .Include(p=>p.User)
                .Include(p=>p.Role);
           
            return entity.ToList();
        }

        public async Task<UserViewModel> RegisterAsync(RegisterRequest request)
        {
            var user = new User
            {
                CompleteName = request.CompleteName,
                UserName = request.Email,
                Email = request.Email,
                IsActive = request.IsActive
            };

            var result = await _userManager.CreateAsync(user, request.Password);
            if (!result.Succeeded)
            {
                throw new Exception(string.Join(", ", result.Errors.Select(e => e.Description)));
            }
             if (!string.IsNullOrEmpty(request.Role))
            {
                await AssignRoleAsync(user.Id.ToString(), request.Role);
            }

            return new UserViewModel
            {
                UserName = user.UserName,
                Email = user.Email,
                CompleteName = user.CompleteName,
                IsActive = user.IsActive
            };
        }
 
        public async Task<string> LoginAsync(LoginRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null || !await _userManager.CheckPasswordAsync(user, request.Password))
            {
                throw new UnauthorizedAccessException("Invalid credentials");
            }
            return await GenerateJWToken(user);
        }
        private async Task<string> GenerateJWToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email, user.Email)
            };

            var roles = await _userManager.GetRolesAsync(user);
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
        
            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = creds
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public async Task<bool> AssignRoleAsync(string userId, string role)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                throw new Exception("User not found");

            var roleExists = await _roleManager.RoleExistsAsync(role);
            if (!roleExists)
                throw new Exception("Role does not exist");

            var result = await _userManager.AddToRoleAsync(user, role);
            var token = await GenerateJWToken(user);
            Console.WriteLine($"Generated Token: {token}");
            return result.Succeeded;
        }
         public async Task<bool> DeleteUserAsync(Guid userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null) return false;

            user.IsActive = false; 
            var result = await _userManager.UpdateAsync(user);

            return result.Succeeded;
        }

        public async Task<UserViewModel> GetByIdAsync(Guid userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null) return null;

            return new UserViewModel
            {
                CompleteName = user.CompleteName,
                Email = user.Email,
                UserName = user.UserName,
                IsActive = user.IsActive
            };
        }
    }
}