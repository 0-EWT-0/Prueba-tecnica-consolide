using API.Config;
using API.Models;
using BCrypt.Net;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using API.Interfaces;
namespace API.Services;


public class AuthService: IAuth
{
    private readonly AppContextDB _context;
    private readonly IConfiguration _configuration;

    
    public AuthService(AppContextDB context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
    }

    private string GenerateJwtToken(UserModel user)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));

        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id_user.ToString()),
            new Claim(ClaimTypes.Name, user.Name),
            new Claim(ClaimTypes.Email, user.Email),
        };

        var TOKEN_EXPIRATION_DAYS = 5;

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddDays(TOKEN_EXPIRATION_DAYS),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public async Task<LoginResponse> LoginAsync(LoginDTO loginDTO)
    {
        try
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == loginDTO.Email.Trim());

            if (user == null)
            {
                return new LoginResponse(false, 404, "User not found");
            }

            bool checkPassword = BCrypt.Net.BCrypt.Verify(loginDTO.Password, user.Password_hash);

            if (!checkPassword)
            {
                return new LoginResponse(false, 401, "Invalid credentials");
            }

            var oldSessions = _context.Session.Where(s => s.Id_user_id == user.Id_user);
            _context.Session.RemoveRange(oldSessions);
            await _context.SaveChangesAsync();

            string token = GenerateJwtToken(user);

            var newSession = new SessionModel
            {
                Session_token = token,
                Created_at = DateTime.UtcNow,
                Id_user_id = user.Id_user
            };

            _context.Session.Add(newSession);
            await _context.SaveChangesAsync();

            return new LoginResponse(true, 200, "Login successful", token);
        } catch (Exception)
        {
            return new LoginResponse(false, 500, "An error occurred while logging in");
        }
    }

    public async Task<RegistationResponse> RegisterAsync(RegisterDTO registerDTO)
    {
        try
        {
            var exists = await _context.Users.AnyAsync(u => u.Email == registerDTO.Email);

            if (exists)
            {
                return new RegistationResponse(false, 409, "User already exists");
            }

            var newUser = new UserModel()
            {
                Name = registerDTO.Name.Trim(),
                Email = registerDTO.Email.Trim(),
                Password_hash = BCrypt.Net.BCrypt.HashPassword(registerDTO.Password),
                Created_at = DateTime.Now,
            };

            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();

            return new RegistationResponse(true, 201, "User registered successfully");
        } catch (Exception)
        {
            return new RegistationResponse(false, 500, "An error occurred while registering the user");
        }
    }

    public async Task<LogoutResponse> LogoutAsync(LogoutDTO logoutDTO)
    {
        try
        {
            var existingSession = await _context.Session.Where(s => s.Id_user_id == logoutDTO.UserId).ToListAsync();

            if (!existingSession.Any())
            {
                return new LogoutResponse(false, 404, "Session not found");
            }

            _context.Session.RemoveRange(existingSession);
            await _context.SaveChangesAsync();

            return new LogoutResponse(true, 200, "Logged out successfully");
        } catch (Exception)
        {
            return new LogoutResponse(false, 500, "An error occurred while logging out");
        }
    }
}
