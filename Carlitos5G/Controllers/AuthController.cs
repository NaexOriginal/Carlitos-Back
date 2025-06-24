using Carlitos5G.Models;
using Carlitos5G.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Carlitos5G.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;

        public AuthController(AuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            if (request == null || string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.Password))
            {
                return BadRequest("Correo y contraseña son requeridos.");
            }

            var response = await _authService.AuthenticateAsync(request.Email, request.Password);
            if (!response.Success || string.IsNullOrEmpty(response.Data))
            {
                return Unauthorized("Correo o contraseña inválidos.");
            }

            return Ok(new { Token = response.Data });

        }

        [HttpGet("authMe")]
        [Authorize] // Aseguramos que el endpoint requiere autenticación
        public async Task<IActionResult> AuthMe()
        {
            var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            if (string.IsNullOrEmpty(token))
            {
                return Unauthorized("Token no proporcionado");
            }

            var userInfo = await _authService.GetUserInfoAsync(token);
            if (userInfo == null)
            {
                return Unauthorized("No se pudo obtener la información del usuario");
            }

            return Ok(userInfo);
        }
    }
}
