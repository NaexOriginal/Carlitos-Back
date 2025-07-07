using Carlitos5G.Commons;
using Carlitos5G.Data;
using Carlitos5G.Dtos;
using Carlitos5G.Models;
using Microsoft.EntityFrameworkCore;

namespace Carlitos5G.Services
{
    public class TutorService : ITutorService
    {
        private readonly ApplicationDbContext _context;
        private readonly ImageUploadService _imageUploadService;

        public TutorService(ApplicationDbContext context, ImageUploadService imageUploadService)
        {
            _context = context;
            _imageUploadService = imageUploadService;
        }

        public async Task<ServiceResponse<IEnumerable<TutorDto>>> GetAllTutorsAsync()
        {
            var response = new ServiceResponse<IEnumerable<TutorDto>>();
            try
            {
                var tutors = await _context.Tutors
                    .Select(t => new TutorDto
                    {
                        Id = t.Id,
                        Name = t.Name,
                        Profession = t.Profession,
                        Email = t.Email,
                        Password = t.Password,
                        Image = t.Image,
                        UpdationDate = t.UpdationDate
                    })
                    .ToListAsync();

                response.Data = tutors;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Error al obtener los tutores.";
                response.ErrorDetails = ex.Message;
            }

            return response;
        }

        public async Task<ServiceResponse<TutorDto>> GetTutorByIdAsync(string id)
        {
            var response = new ServiceResponse<TutorDto>();
            try
            {
                var tutor = await _context.Tutors
                    .Where(t => t.Id.ToString() == id)
                    .Select(t => new TutorDto
                    {
                        Id = t.Id,
                        Name = t.Name,
                        Profession = t.Profession,
                        Email = t.Email,
                        Password = t.Password,
                        Image = t.Image,
                        UpdationDate = t.UpdationDate
                    })
                    .FirstOrDefaultAsync();

                if (tutor == null)
                {
                    response.Success = false;
                    response.Message = "Tutor no encontrado.";
                }
                else
                {
                    response.Data = tutor;
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Error al obtener el tutor.";
                response.ErrorDetails = ex.Message;
            }

            return response;
        }

        public async Task<ServiceResponse<TutorDto>> CreateTutorAsync(TutorDto tutorDto)
        {
            var response = new ServiceResponse<TutorDto>();
            try
            {
                //* Verificamos si el correo existe
                var existingTutor = await _context.Tutors.FirstOrDefaultAsync(t => t.Email == tutorDto.Email);

                if (existingTutor != null)
                {
                    response.Success = false;
                    response.Message = "El correo electrónico ya está registrado";
                    return response;
                }

                string? imageUrl = null;
                if (tutorDto.ImageFile != null)
                {
                    imageUrl = await _imageUploadService.UploadImageAsync(tutorDto.ImageFile);
                }
                else
                {
                    imageUrl = "Default.jpg";
                }

                var tutor = new Tutor
                {
                    Name = tutorDto.Name,
                    Profession = tutorDto.Profession,
                    Email = tutorDto.Email,
                    Password = SHA256Helper.ComputeSHA256Hash(tutorDto.Password),
                    Image = imageUrl,
                    UpdationDate = DateTime.UtcNow
                };

                _context.Tutors.Add(tutor);
                await _context.SaveChangesAsync();

                response.Data = new TutorDto
                {
                    Id = tutor.Id,
                    Name = tutor.Name,
                    Profession = tutor.Profession,
                    Email = tutor.Email,
                    Image = tutor.Image,
                    UpdationDate = tutor.UpdationDate
                };
                response.Message = "Tutor creado exitosamente.";
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Error al crear el tutor.";
                response.ErrorDetails = ex.Message;
            }

            return response;
        }

        public async Task<ServiceResponse<TutorDto>> UpdateTutorAsync(string id, TutorDto tutorDto)
        {
            var response = new ServiceResponse<TutorDto>();
            try
            {
                //* Convertir el ID a Guid para FindAsync (si no lo hiciste ya)
                if (!Guid.TryParse(id, out Guid tutorGuidId))
                {
                    response.Success = false;
                    response.Message = "ID de tutor inválido";
                    return response;
                }

                var existingTutor = await _context.Tutors.FindAsync(tutorGuidId);
                if (existingTutor == null)
                {
                    response.Success = false;
                    response.Message = "Tutor no encontrado.";
                    return response;
                }

                existingTutor.Name = tutorDto.Name;
                existingTutor.Profession = tutorDto.Profession;
                existingTutor.Email = tutorDto.Email;

                if (!string.IsNullOrWhiteSpace(tutorDto.Password))
                {
                    existingTutor.Password = SHA256Helper.ComputeSHA256Hash(tutorDto.Password);
                }
                existingTutor.UpdationDate = DateTime.UtcNow;

                if (tutorDto.ImageFile != null)
                {
                    string imageUrl = await _imageUploadService.UploadImageAsync(tutorDto.ImageFile);
                    existingTutor.Image = imageUrl;
                }
                else if (string.IsNullOrEmpty(existingTutor.Image) || existingTutor.Image != "default.jpg")
                {
                    existingTutor.Image = "default.jpg";
                }

                await _context.SaveChangesAsync();

                response.Data = new TutorDto
                {
                    Id = existingTutor.Id,
                    Name = existingTutor.Name,
                    Profession = existingTutor.Profession,
                    Email = existingTutor.Profession,
                    Image = existingTutor.Image,
                    UpdationDate = existingTutor.UpdationDate,
                    IsEdit = tutorDto.IsEdit
                };
                response.Success = true;
                response.Message = "Tutor actualizado exitosamente.";
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Error al actualizar el tutor.";
                response.ErrorDetails = ex.Message;
            }

            return response;
        }

        public async Task<ServiceResponse<bool>> DeleteTutorAsync(string id)
        {
            var response = new ServiceResponse<bool>();
            try
            {
                var tutor = await _context.Tutors.FirstOrDefaultAsync(t => t.Id.ToString() == id);
                if (tutor == null)
                {
                    response.Success = false;
                    response.Message = "Tutor no encontrado.";
                    response.Data = false;
                    return response;
                }

                _context.Tutors.Remove(tutor);
                await _context.SaveChangesAsync();

                response.Data = true;
                response.Message = "Tutor eliminado exitosamente.";
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Error al eliminar el tutor.";
                response.ErrorDetails = ex.Message;
                response.Data = false;
            }

            return response;
        }

        public async Task<ServiceResponse<IEnumerable<TutorDto>>> SearchTutorsAsync(string searchTerm)
        {
            var response = new ServiceResponse<IEnumerable<TutorDto>>();
            try
            {
                searchTerm = searchTerm.ToLower();
                var tutors = await _context.Tutors
                    .Where(t => t.Name.ToLower().Contains(searchTerm) ||
                                t.Profession.ToLower().Contains(searchTerm) ||
                                t.Email.ToLower().Contains(searchTerm))
                    .Select(t => new TutorDto
                    {
                        Id = t.Id,
                        Name = t.Name,
                        Profession = t.Profession,
                        Email = t.Email,
                        Password = t.Password,
                        Image = t.Image,
                        UpdationDate = t.UpdationDate
                    })
                    .ToListAsync();

                response.Data = tutors;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Error al buscar tutores.";
                response.ErrorDetails = ex.Message;
            }

            return response;
        }

        public async Task<ServiceResponse<TutorStatsDto>> GetTutorStatsAsync(string id)
        {
            var response = new ServiceResponse<TutorStatsDto>();
            try
            {
                if (!Guid.TryParse(id, out Guid tutorId))
                {
                    response.Success = false;
                    response.Message = "ID de tutor inválido.";
                    return response;
                }

                var tutorExists = await _context.Tutors.AnyAsync(t => t.Id == tutorId);
                if (!tutorExists)
                {
                    response.Success = false;
                    response.Message = "Tutor no encontrado.";
                    return response;
                }

                var playlistCount = await _context.Playlists.CountAsync(p => p.TutorId == tutorId);
                var contentCount = await _context.Contents.CountAsync(c => c.TutorId == tutorId);
                var likeCount = await _context.Likes.CountAsync(l => l.TutorId == tutorId);
                var commentCount = await _context.Comments.CountAsync(c => c.TutorId == tutorId);

                response.Data = new TutorStatsDto
                {
                    TutorId = tutorId,
                    TotalPlaylists = playlistCount,
                    TotalContents = contentCount,
                    TotalLikes = likeCount,
                    TotalComments = commentCount
                };
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Error al obtener estadísticas del tutor.";
                response.ErrorDetails = ex.Message;
            }

            return response;
        }

        public async Task<ServiceResponse<IEnumerable<PlaylistDto>>> GetTutorCoursesAsync(string tutorId)
        {
            var response = new ServiceResponse<IEnumerable<PlaylistDto>>();
            try
            {
                if (!Guid.TryParse(tutorId, out Guid id))
                {
                    response.Success = false;
                    response.Message = "ID de tutor inválido.";
                    response.Data = new List<PlaylistDto>();
                    return response;
                }

                var playlists = await _context.Playlists
                    .Where(p => p.TutorId == id && p.Status == "active")
                    .Select(p => new PlaylistDto
                    {
                        Id = p.Id,
                        Title = p.Title,
                        Thumb = p.Thumb,
                        Date = p.Date,
                        TutorId = p.TutorId
                    })
                    .ToListAsync();

                response.Data = playlists;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Error al obtener los cursos del tutor.";
                response.ErrorDetails = ex.Message;
                response.Data = new List<PlaylistDto>();
            }

            return response;
        }
    }
}
