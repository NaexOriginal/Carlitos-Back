using Carlitos5G.Commons;
using Carlitos5G.Data;
using Carlitos5G.Dtos;
using Carlitos5G.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Carlitos5G.Services
{
    public class AuthService : IAuthService
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly IAuditService _auditService;


        public AuthService(ApplicationDbContext context, IConfiguration configuration, IAuditService auditService)
        {
            _context = context;
            _configuration = configuration;
            _auditService = auditService;
        }

        public async Task<ServiceResponse<string>> AuthenticateAsync(string email, string password)
        {
            var response = new ServiceResponse<string>();

            try
            {
                string hashedPassword = SHA256Helper.ComputeSHA256Hash(password);

                var admin = await _context.Admins.FirstOrDefaultAsync(a => a.Email == email && a.Password == hashedPassword);
                if (admin != null)
                {
                    var token = GenerateJwt(admin.Id.ToString(), "Admin");
                    response.Data = token;

                    await _auditService.LogAsync(
                        "Admins",
                        "Login",
                        admin.Id.ToString(),
                        $"Login exitoso para el administrador con email: {email}",
                        email
                    );

                    return response;
                }

                var tutor = await _context.Tutors.FirstOrDefaultAsync(t => t.Email == email && t.Password == hashedPassword);
                if (tutor != null)
                {
                    var token = GenerateJwt(tutor.Id.ToString(), "Tutor");
                    response.Data = token;

                    await _auditService.LogAsync(
                        "Tutors",
                        "Login",
                        tutor.Id.ToString(),
                        $"Login exitoso para el tutor con email: {email}",
                        email
                    );

                    return response;
                }

                var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email && u.Password == hashedPassword);
                if (user != null)
                {
                    var token = GenerateJwt(user.Id.ToString(), "User");
                    response.Data = token;

                    await _auditService.LogAsync(
                        "Users",
                        "Login",
                        user.Id.ToString(),
                        $"Login exitoso para el usuario con email: {email}",
                        email
                    );

                    return response;
                }

                // Login fallido
                response.Success = false;
                response.Message = "Credenciales inválidas. Por favor, verifica tu correo y contraseña.";

                await _auditService.LogAsync(
                    "Auth",
                    "FailedLogin",
                    email,
                    "Intento fallido de inicio de sesión",
                    email
                );
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Error al intentar autenticar.";
                response.ErrorDetails = ex.Message;

                await _auditService.LogAsync(
                    "Auth",
                    "Error",
                    email,
                    $"Error en autenticación: {ex.Message}",
                    email
                );
            }

            return response;
        }




        private string GenerateJwt(string id, string role)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, id),
                new Claim(ClaimTypes.Role, role)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"])); // Lee la clave secreta de la configuración
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<object> GetUserInfoAsync(string token)
        {
            var principal = GetPrincipalFromToken(token);
            if (principal == null)
            {
                return null; // Token inválido o no encontrado
            }

            var userId = principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var role = principal.FindFirst(ClaimTypes.Role)?.Value; // Obtener el rol desde los claims

            if (userId == null || role == null)
            {
                return null; // El token no contiene un ID de usuario o rol
            }

            // Dependiendo del rol, buscamos en la base de datos el modelo adecuado
            switch (role)
            {
                case "Admin":
                    var admin = await _context.Admins.FirstOrDefaultAsync(a => a.Id.ToString() == userId);
                    if (admin == null) return null; // No se encuentra el admin
                    return new Admin
                    {
                        Id = admin.Id,
                        Email = admin.Email,
                        Name = admin.Name,
                    };

                case "Tutor":
                    var tutor = await _context.Tutors.FirstOrDefaultAsync(t => t.Id.ToString() == userId);
                    if (tutor == null) return null; // No se encuentra el tutor
                    return new TutorDto
                    {
                        Id = tutor.Id,
                        Email = tutor.Email,
                        Name = tutor.Name,
                    };

                case "User":
                    var user = await _context.Users.FirstOrDefaultAsync(u => u.Id.ToString() == userId);
                    if (user == null) return null; // No se encuentra el usuario
                    return new UserDto
                    {
                        Id = user.Id,
                        Email = user.Email,
                        Name = user.Name,
                    };

                default:
                    return null; // Rol desconocido
            }
        }

        private ClaimsPrincipal GetPrincipalFromToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                var principal = tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = _configuration["Jwt:Issuer"],
                    ValidAudience = _configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"])),
                    ValidateLifetime = true, // Verifica si el token no ha expirado
                }, out var validatedToken);

                return principal;
            }
            catch (Exception)
            {
                return null; // Si ocurre un error al validar el token, devolvemos null
            }
        }
    }
}
