using Carlitos5G.Commons;
using Carlitos5G.Data;
using Carlitos5G.Dtos;
using Carlitos5G.Models;
using Microsoft.EntityFrameworkCore;

namespace Carlitos5G.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;
        private readonly ImageUploadService _imageUploadService;

        public UserService(ApplicationDbContext context, ImageUploadService imageUploadService)
        {
            _context = context;
            _imageUploadService = imageUploadService;
        }

        public async Task<ServiceResponse<IEnumerable<UserDto>>> GetAllUsersAsync()
        {
            var response = new ServiceResponse<IEnumerable<UserDto>>();
            try
            {
                var users = await _context.Users.ToListAsync();

                response.Data = users.Select(user => new UserDto
                {
                    Id = user.Id,
                    Name = user.Name,
                    Email = user.Email,
                    Password = user.Password,
                    Image = user.Image,
                    Genero = user.Genero,
                    Telefono = user.Telefono,
                    FechaAsig = user.FechaAsig,
                    EtapaEducativa = user.EtapaEducativa,
                    Grado = user.Grado,
                    UpdationDate = user.UpdationDate
                }).ToList();
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Error al obtener los usuarios.";
                response.ErrorDetails = ex.Message;
            }
            return response;
        }

        public async Task<ServiceResponse<UserDto>> GetUserByIdAsync(string id)
        {
            var response = new ServiceResponse<UserDto>();
            try
            {
                var user = await _context.Users
                    .Where(u => u.Id.ToString() == id)
                    .FirstOrDefaultAsync();

                if (user == null)
                {
                    response.Success = false;
                    response.Message = "Usuario no encontrado.";
                }
                else
                {
                    response.Data = new UserDto
                    {
                        Id = user.Id,
                        Name = user.Name,
                        Email = user.Email,
                        Password = user.Password,
                        Image = user.Image,
                        Genero = user.Genero,
                        Telefono = user.Telefono,
                        FechaAsig = user.FechaAsig,
                        EtapaEducativa = user.EtapaEducativa,
                        Grado = user.Grado,
                        UpdationDate = user.UpdationDate
                    };
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Error al obtener el usuario.";
                response.ErrorDetails = ex.Message;
            }
            return response;
        }

        public async Task<ServiceResponse<UserDto>> CreateUserAsync(UserDto userDto)
        {
            var response = new ServiceResponse<UserDto>();
            try
            {
                string? imageUrl = null;
                if (userDto.ImageFile != null)
                {
                    imageUrl = await _imageUploadService.UploadImageAsync(userDto.ImageFile);
                }

                var user = new User
                {
                    Id = userDto.Id,
                    Name = userDto.Name,
                    Email = userDto.Email,
                    Password = SHA256Helper.ComputeSHA256Hash(userDto.Password),
                    Image = imageUrl,
                    Genero = userDto.Genero,
                    Telefono = userDto.Telefono,
                    FechaAsig = userDto.FechaAsig,
                    EtapaEducativa = userDto.EtapaEducativa,
                    Grado = userDto.Grado
                };

                _context.Users.Add(user);
                await _context.SaveChangesAsync();

                response.Data = userDto;
                response.Message = "Usuario creado exitosamente.";
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Error al crear el usuario.";
                response.ErrorDetails = ex.Message;
            }
            return response;
        }

        public async Task<ServiceResponse<UserDto>> UpdateUserAsync(string id, UserDto userDto)
        {
            var response = new ServiceResponse<UserDto>();
            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(t => t.Id.ToString() == id);
                if (user == null)
                {
                    response.Success = false;
                    response.Message = "Usuario no encontrado.";
                    return response;
                }

                user.Name = userDto.Name;
                user.Email = userDto.Email;

                // Si se actualiza la contraseña, la hasheamos
                if (!string.IsNullOrWhiteSpace(userDto.Password))
                {
                    user.Password = SHA256Helper.ComputeSHA256Hash(userDto.Password);
                }

                user.Genero = userDto.Genero;
                user.Telefono = userDto.Telefono;
                user.FechaAsig = userDto.FechaAsig;
                user.EtapaEducativa = userDto.EtapaEducativa;
                user.Grado = userDto.Grado;
                user.UpdationDate = userDto.UpdationDate;

                // Manejo de imagen
                if (userDto.ImageFile != null)
                {
                    user.Image = await _imageUploadService.UploadImageAsync(userDto.ImageFile);
                }
                else if (string.IsNullOrEmpty(user.Image))
                {
                    user.Image = "default.jpg"; // Imágen por defecto si no se proporciona una imagen
                }

                _context.Users.Update(user);
                await _context.SaveChangesAsync();

                response.Data = userDto;
                response.Message = "Usuario actualizado exitosamente.";
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Error al actualizar el usuario.";
                response.ErrorDetails = ex.Message;
            }
            return response;
        }

        public async Task<ServiceResponse<bool>> DeleteUserAsync(string id)
        {
            var response = new ServiceResponse<bool>();
            try
            {
                var user = await _context.Users.FindAsync(id);
                if (user == null)
                {
                    response.Success = false;
                    response.Message = "Usuario no encontrado.";
                    response.Data = false;
                    return response;
                }

                _context.Users.Remove(user);
                await _context.SaveChangesAsync();

                response.Data = true;
                response.Message = "Usuario eliminado exitosamente.";
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Error al eliminar el usuario.";
                response.ErrorDetails = ex.Message;
                response.Data = false;
            }
            return response;
        }

        public async Task<ServiceResponse<UserDto>> FindUserByIdOrEmailAsync(string searchTerm)
        {
            var response = new ServiceResponse<UserDto>();
            try
            {
                var user = await _context.Users
                    .FirstOrDefaultAsync(u => u.Id.ToString() == searchTerm || u.Email == searchTerm);

                if (user == null)
                {
                    response.Success = false;
                    response.Message = "Usuario no encontrado.";
                }
                else
                {
                    response.Data = new UserDto
                    {
                        Id = user.Id,
                        Name = user.Name,
                        Email = user.Email,
                        Image = user.Image,
                        Genero = user.Genero,
                        Telefono = user.Telefono,
                        FechaAsig = user.FechaAsig,
                        EtapaEducativa = user.EtapaEducativa,
                        Grado = user.Grado
                    };
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Error al buscar el usuario.";
                response.ErrorDetails = ex.Message;
            }
            return response;
        }

        public async Task<ServiceResponse<UserActivityDto>> GetUserActivityAsync(string userId)
        {
            var response = new ServiceResponse<UserActivityDto>();
            try
            {
                if (!Guid.TryParse(userId, out Guid userGuid))
                {
                    response.Success = false;
                    response.Message = "ID de usuario inválido.";
                    return response;
                }

                var userExists = await _context.Users.AnyAsync(u => u.Id == userGuid);
                if (!userExists)
                {
                    response.Success = false;
                    response.Message = "Usuario no encontrado.";
                    return response;
                }

                var totalLikes = await _context.Likes.CountAsync(l => l.UserId == userGuid);
                var totalComments = await _context.Comments.CountAsync(c => c.UserId == userGuid);
                var totalBookmarked = await _context.Bookmarks.CountAsync(b => b.UserId == userGuid);

                response.Data = new UserActivityDto
                {
                    TotalLikes = totalLikes,
                    TotalComments = totalComments,
                    TotalBookmarked = totalBookmarked
                };
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Error al obtener la actividad del usuario.";
                response.ErrorDetails = ex.Message;
            }
            return response;
        }

        public async Task<ServiceResponse<bool>> DeleteUserWithRelatedDataAsync(string userId)
        {
            var response = new ServiceResponse<bool>();

            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                if (!Guid.TryParse(userId, out Guid userGuid))
                {
                    response.Success = false;
                    response.Message = "ID de usuario inválido.";
                    return response;
                }

                // 1. Eliminar bookmarks (inscripciones a cursos)
                var bookmarks = await _context.Bookmarks
                    .Where(b => b.UserId == userGuid)
                    .ToListAsync();
                _context.Bookmarks.RemoveRange(bookmarks);

                // 2. Eliminar comentarios
                var comments = await _context.Comments
                    .Where(c => c.UserId == userGuid)
                    .ToListAsync();
                _context.Comments.RemoveRange(comments);

                // 3. Eliminar likes
                var likes = await _context.Likes
                    .Where(l => l.UserId == userGuid)
                    .ToListAsync();
                _context.Likes.RemoveRange(likes);

                // 4. Eliminar registros de avance
                var avances = await _context.Avances
                    .Where(a => a.UserId == userGuid)
                    .ToListAsync();
                _context.Avances.RemoveRange(avances);

                // 5. Eliminar resultados (tblresult)
                var resultados = await _context.Tblresults
                    .Where(r => r.StudentId == userGuid)
                    .ToListAsync();
                _context.Tblresults.RemoveRange(resultados);

                // 6. Finalmente eliminar el usuario
                var user = await _context.Users.FindAsync(userGuid);
                if (user != null)
                {
                    _context.Users.Remove(user);
                }

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                response.Data = true;
                response.Message = "Usuario y todos sus datos relacionados eliminados exitosamente.";
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                response.Success = false;
                response.Message = "Error al eliminar el usuario y sus datos relacionados.";
                response.ErrorDetails = ex.Message;
                response.Data = false;
            }

            return response;
        }


        public async Task<ServiceResponse<StudentCoursesDto>> GetStudentCoursesWithProgressAsync(string userId)
        {
            var response = new ServiceResponse<StudentCoursesDto>();
            try
            {
                if (!Guid.TryParse(userId, out Guid userGuid))
                {
                    response.Success = false;
                    response.Message = "ID de usuario inválido.";
                    return response;
                }

                var user = await _context.Users.FindAsync(userGuid);
                if (user == null)
                {
                    response.Success = false;
                    response.Message = "Usuario no encontrado.";
                    return response;
                }

                var result = new StudentCoursesDto
                {
                    UserId = user.Id,
                    UserName = user.Name
                };

                // Obtener cursos inscritos (bookmarks)
                var enrolledCourses = await _context.Bookmarks
                    .Where(b => b.UserId == userGuid)
                    .Include(b => b.Playlist)
                    .ToListAsync();

                foreach (var bookmark in enrolledCourses)
                {
                    if (bookmark.Playlist == null) continue;

                    // Obtener actividades del curso (content + flipbook)
                    var contentActivities = await _context.Contents
                        .Where(c => c.PlaylistId == bookmark.PlaylistId)
                        .CountAsync();

                    var flipbookActivities = await _context.Flipbooks
                        .Where(f => f.PlaylistId == bookmark.PlaylistId)
                        .CountAsync();

                    var totalActivities = contentActivities + flipbookActivities;

                    // Obtener progreso completado
                    var completedActivities = await _context.Avances
                        .Where(a => a.UserId == userGuid && a.PlaylistId == bookmark.PlaylistId)
                        .CountAsync();

                    // Calcular porcentaje
                    var progressPercentage = totalActivities > 0 ?
                        (int)Math.Round((double)completedActivities / totalActivities * 100) : 0;

                    result.Courses.Add(new CourseProgressDto
                    {
                        PlaylistId = bookmark.PlaylistId,
                        Title = bookmark.Playlist.Title,
                        Thumbnail = bookmark.Playlist.Thumb,
                        TotalActivities = totalActivities,
                        CompletedActivities = completedActivities,
                        ProgressPercentage = progressPercentage
                    });
                }

                response.Data = result;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Error al obtener los cursos del estudiante.";
                response.ErrorDetails = ex.Message;
            }
            return response;
        }

        public async Task<ServiceResponse<List<AvailableCourseDto>>> GetAvailableCoursesForStudentAsync(string userId)
        {
            var response = new ServiceResponse<List<AvailableCourseDto>>();

            try
            {
                if (!Guid.TryParse(userId, out Guid userGuid))
                {
                    response.Success = false;
                    response.Message = "ID de usuario inválido.";
                    return response;
                }

                // 1. Obtener IDs de cursos en los que ya está inscrito el usuario
                var enrolledCourseIds = await _context.Bookmarks
                    .Where(b => b.UserId == userGuid)
                    .Select(b => b.PlaylistId)
                    .ToListAsync();

                // 2. Obtener cursos donde el usuario NO está inscrito
                var availableCourses = await _context.Playlists
                    .Where(p => !enrolledCourseIds.Contains(p.Id))
                    .Select(p => new AvailableCourseDto
                    {
                        PlaylistId = p.Id,
                        Title = p.Title,
                        Thumbnail = p.Thumb,
                        Description = p.Description
                    })
                    .ToListAsync();

                response.Data = availableCourses;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Error al obtener los cursos disponibles.";
                response.ErrorDetails = ex.Message;
            }

            return response;
        }


    }
}
