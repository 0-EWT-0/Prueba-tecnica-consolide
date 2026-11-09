namespace API.Interfaces;

public record LoginResponse(bool Flag, int Code, string Message = null!, string Token = null!);
public record RegistationResponse(bool Flag, int Code, string Message = null!);
public record LogoutResponse(bool Flag, int Code, string Message = null!);

public record LoginDTO(string Email, string Password);
public record RegisterDTO(string Name, string Email, string Password);
public record LogoutDTO(int UserId);

public interface IAuth
{
    Task<LoginResponse> LoginAsync(LoginDTO loginDTO);
    Task<RegistationResponse> RegisterAsync(RegisterDTO registerDTO);
    Task<LogoutResponse> LogoutAsync(LogoutDTO logoutDTO);
}
