

using API.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;


namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuth _authService;
        public AuthController(IAuth authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<ActionResult<LoginResponse>> Login([FromBody] LoginDTO loginDTO)
        {
            if (loginDTO == null)
            {
                return BadRequest(new LoginResponse(false, 400, "Invalid request"));
            }

            var result = await _authService.LoginAsync(loginDTO);

            if (result.Code == 200 && result.Flag == true)
            {
                return Ok(result);
            }

            return StatusCode(result.Code, result);

        }

        [HttpPost("register")]
        public async Task<ActionResult<RegistationResponse>> Register([FromBody] RegisterDTO registerDTO)
        {
            if (registerDTO == null)
            {
                return BadRequest(new RegistationResponse(false, 400, "Invalid request"));
            }

            var result = await _authService.RegisterAsync(registerDTO);

            return StatusCode(result.Code, result);
        }

        [Authorize]
        [HttpPost("logout")]
        public async Task<ActionResult<LogoutResponse>> Logout()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            bool couldConvert = int.TryParse(userIdClaim, out int userId);

            if (couldConvert == false)
            {
                return BadRequest(new LogoutResponse(false, 400, "Invalid request"));
            }

            var result = await _authService.LogoutAsync(new LogoutDTO(userId));

            return StatusCode(result.Code, result);
        }
    }
}
