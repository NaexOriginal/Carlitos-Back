using Carlitos5G.Commons;
using Carlitos5G.Dtos;
using Carlitos5G.Services;
using Microsoft.AspNetCore.Mvc;

namespace Carlitos5G.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        // Obtener todos los usuarios
        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userService.GetAllUsersAsync();
            return Ok(users);
        }

        // Obtener un usuario por ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(string id)
        {
            var user = await _userService.GetUserByIdAsync(id);

            if (user == null)
                return NotFound();

            return Ok(user);
        }

        // Crear un nuevo usuario
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromForm] UserDto userDto)
        {
            try
            {
                var createdUser = await _userService.CreateUserAsync(userDto);
                return CreatedAtAction(nameof(GetUserById), new { id = createdUser.Data.Id }, createdUser.Data);
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    Message = ex.Message,
                    Inner = ex.InnerException?.Message
                });
            }
        }

        // Actualizar un usuario
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(string id, [FromForm] UserDto userDto)
        {
            try
            {
                userDto.IsEdit = true;

                var updatedUser = await _userService.UpdateUserAsync(id, userDto);
                if (updatedUser == null)
                    return NotFound();

                return Ok(updatedUser);
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    Message = ex.Message,
                    Inner = ex.InnerException?.Message
                });
            }
        }

        // Eliminar un usuario
        [HttpDelete("{id}")]
        public async Task<ServiceResponse<bool>> DeleteUserAsync(string id)
        {
            var response = new ServiceResponse<bool>();

            if (!Guid.TryParse(id, out Guid userId))
            {
                response.Data = false;
                response.Success = false;
                response.Message = "ID de usuario inválido";
                return response;
            }

            var result = await _userService.DeleteUserAsync(id);
            if (!result.Success || !result.Data)
            {
                response.Data = false;
                response.Success = false;
                response.Message = "Usuario no encontrado o no se pudo eliminar";
                return response;
            }

            response.Data = true;
            response.Success = true;
            return response;
        }

        // Agrega estos endpoints al UserController

        [HttpGet("search/{searchTerm}")]
        public async Task<IActionResult> FindUserByIdOrEmail(string searchTerm)
        {
            var result = await _userService.FindUserByIdOrEmailAsync(searchTerm);
            if (!result.Success) return NotFound(result.Message);
            return Ok(result);
        }

        [HttpGet("{userId}/activity")]
        public async Task<IActionResult> GetUserActivity(string userId)
        {
            var result = await _userService.GetUserActivityAsync(userId);
            if (!result.Success) return NotFound(result.Message);
            return Ok(result);
        }

        [HttpGet("{userId}/courses")]
        public async Task<IActionResult> GetStudentCoursesWithProgress(string userId)
        {
            var result = await _userService.GetStudentCoursesWithProgressAsync(userId);
            if (!result.Success) return NotFound(result.Message);
            return Ok(result);
        }

        [HttpDelete("{userId}/with-data")]
        public async Task<IActionResult> DeleteUserWithRelatedData(string userId)
        {
            var result = await _userService.DeleteUserWithRelatedDataAsync(userId);
            if (!result.Success) return BadRequest(result.Message);
            return Ok(result);
        }

        [HttpGet("{userId}/available-courses")]
        public async Task<IActionResult> GetAvailableCoursesForStudent(string userId)
        {
            var result = await _userService.GetAvailableCoursesForStudentAsync(userId);
            if (!result.Success) return BadRequest(result.Message);
            return Ok(result);
        }

    }
}
