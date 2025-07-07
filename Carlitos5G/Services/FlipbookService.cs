using Carlitos5G.Data;
using Carlitos5G.Dtos;
using Carlitos5G.Models;
using Carlitos5G.Commons;
using Microsoft.EntityFrameworkCore;
using PdfSharpCore.Pdf;

namespace Carlitos5G.Services
{
    public class FlipbookService : IFlipbookService
    {
        private readonly ApplicationDbContext _context;
        private readonly FileUploadService _fileUploadService;
        private readonly ImageUploadService _imageUploadService;

        public FlipbookService(ApplicationDbContext context, FileUploadService fileUploadService, ImageUploadService imageUploadService)
        {
            _context = context;
            _fileUploadService = fileUploadService;
            _imageUploadService = imageUploadService;
        }

        public async Task<ServiceResponse<IEnumerable<FlipbookDto>>> GetAllFlipbooksAsync()
        {
            var response = new ServiceResponse<IEnumerable<FlipbookDto>>();
            try
            {
                var flipbooks = await _context.Flipbooks
                    .Select(f => new FlipbookDto
                    {
                        Id = f.Id,
                        TutorId = f.TutorId,
                        PlaylistId = f.PlaylistId,
                        Title = f.Title,
                        Description = f.Description,
                        Pages = f.Pages,
                        Directory = f.Directory,
                        MediaPath = f.MediaPath,
                        ThumbnailPath = f.ThumbnailPath,
                        Status = f.Status,
                        Type = f.Type
                    }).ToListAsync();

                response.Data = flipbooks;
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Error al obtener los flipbooks.";
                response.ErrorDetails = ex.Message;
            }

            return response;
        }

        public async Task<ServiceResponse<FlipbookDto>> GetFlipbookByIdAsync(string id)
        {
            var response = new ServiceResponse<FlipbookDto>();
            try
            {
                var flipbook = await _context.Flipbooks
                    .Where(f => f.Id.ToString() == id)
                    .Select(f => new FlipbookDto
                    {
                        Id = f.Id,
                        TutorId = f.TutorId,
                        PlaylistId = f.PlaylistId,
                        Title = f.Title,
                        Description = f.Description,
                        Pages = f.Pages,
                        Directory = f.Directory,
                        MediaPath = f.MediaPath,
                        ThumbnailPath = f.ThumbnailPath,
                        Status = f.Status,
                        Type = f.Type
                    }).FirstOrDefaultAsync();

                if (flipbook == null)
                {
                    response.Success = false;
                    response.Message = "Flipbook no encontrado.";
                }
                else
                {
                    response.Data = flipbook;
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Error al obtener el flipbook.";
                response.ErrorDetails = ex.Message;
            }

            return response;
        }

        public async Task<ServiceResponse<FlipbookDto>> CreateFlipbookAsync(FlipbookDto dto)
        {
            var response = new ServiceResponse<FlipbookDto>();
            try
            {

                string? pdfUrl = null;
                int pageCount = 0;
                string? directoryPath = null;
                string? imageUrl = null;

                if (dto.MediaFile != null && dto.MediaFile.Length > 0)
                {
                    var pdfUploadResult = await _fileUploadService.UploadAndConvertPdfAsync(dto.MediaFile);

                    if (pdfUploadResult == null || string.IsNullOrEmpty(pdfUploadResult.PdfUrl))
                    {
                        response.Success = false;
                        response.Message = "Error al subir o procesar el archivo PDF. La URL del PDF es nula o vacía";
                        return response;
                    }

                    pdfUrl = pdfUploadResult.PdfUrl;
                    pageCount = pdfUploadResult.PageCount;

                    //* Aseguramos que PdfUrl no es nulo antes de llamar a Replace
                    directoryPath = pdfUrl.Replace(".pdf", "");
                }
                else
                {
                    response.Success = false;
                    response.Message = "El archivo PDF del flipbook es obligatorio y no fue proporcionado o está vacío";
                    return response;
                }

                if (dto.ThumbnailFile != null)
                    imageUrl = await _imageUploadService.UploadImageAsync(dto.ThumbnailFile);

                var flipbook = new Flipbook
                {
                    Id = dto.Id,
                    TutorId = dto.TutorId,
                    PlaylistId = dto.PlaylistId,
                    Title = dto.Title,
                    Description = dto.Description,
                    Pages = dto.Pages ?? pageCount,
                    MediaPath = pdfUrl,
                    ThumbnailPath = imageUrl ?? dto.ThumbnailPath,
                    Directory = directoryPath,
                    Status = dto.Status,
                    Type = dto.Type
                };

                _context.Flipbooks.Add(flipbook);
                await _context.SaveChangesAsync();

                response.Success = true;
                response.Data = dto;
                response.Message = "Flipbook creado exitosamente.";
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Error al crear el flipbook.";
                response.ErrorDetails = ex.Message;
            }

            return response;
        }

        public async Task<ServiceResponse<bool>> UpdateFlipbookAsync(string id, FlipbookDto dto)
        {
            var response = new ServiceResponse<bool>();

            try
            {
                var flipbook = await _context.Flipbooks.FindAsync(Guid.Parse(id));
                if (flipbook == null)
                {
                    response.Success = false;
                    response.Message = "Flipbook no encontrado.";
                    response.Data = false;
                    return response;
                }

                // Si se sube un nuevo archivo PDF, se actualiza y se recalculan las páginas
                if (dto.MediaFile != null)
                {
                    var result = await _fileUploadService.UploadAndConvertPdfAsync(dto.MediaFile);
                    flipbook.MediaPath = result.PdfUrl;
                    flipbook.Directory = result.PdfUrl.Replace(".pdf", "");
                    flipbook.Pages = result.PageCount; // Actualiza el número de páginas
                }

                // Si se sube una nueva imagen miniatura
                if (dto.ThumbnailFile != null)
                {
                    string imageUrl = await _imageUploadService.UploadImageAsync(dto.ThumbnailFile);
                    flipbook.ThumbnailPath = imageUrl;
                }

                // Actualizar campos restantes
                flipbook.Title = dto.Title;
                flipbook.Description = dto.Description;
                flipbook.TutorId = dto.TutorId;
                flipbook.PlaylistId = dto.PlaylistId;
                flipbook.Status = dto.Status;
                flipbook.Type = dto.Type;

                // Solo actualiza Pages si no viene PDF nuevo y dto.Pages tiene valor
                if (dto.MediaFile == null && dto.Pages.HasValue)
                {
                    flipbook.Pages = dto.Pages.Value;
                }

                await _context.SaveChangesAsync();

                response.Data = true;
                response.Message = "Flipbook actualizado exitosamente.";
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Error al actualizar el flipbook.";
                response.ErrorDetails = ex.Message;
                response.Data = false;
            }

            return response;
        }


        public async Task<ServiceResponse<bool>> DeleteFlipbookAsync(string id)
        {
            var response = new ServiceResponse<bool>();
            try
            {
                var flipbook = await _context.Flipbooks.FindAsync(Guid.Parse(id));
                if (flipbook == null)
                {
                    response.Success = false;
                    response.Message = "Flipbook no encontrado.";
                    response.Data = false;
                    return response;
                }

                _context.Flipbooks.Remove(flipbook);
                await _context.SaveChangesAsync();

                response.Data = true;
                response.Message = "Flipbook eliminado exitosamente.";
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Error al eliminar el flipbook.";
                response.ErrorDetails = ex.Message;
                response.Data = false;
            }

            return response;
        }

        public async Task<ServiceResponse<IEnumerable<FlipbookDto>>> GetFlipbooksByPlaylistIdAsync(string playlistId)
        {
            var response = new ServiceResponse<IEnumerable<FlipbookDto>>();

            try
            {
                var playlistGuid = Guid.Parse(playlistId);

                var flipbooks = await _context.Flipbooks
                    .Where(f => f.PlaylistId == playlistGuid)
                    .Select(f => new FlipbookDto
                    {
                        Id = f.Id,
                        TutorId = f.TutorId,
                        PlaylistId = f.PlaylistId,
                        Title = f.Title,
                        Description = f.Description,
                        Pages = f.Pages,
                        Directory = f.Directory,
                        MediaPath = f.MediaPath,
                        ThumbnailPath = f.ThumbnailPath,
                        Status = f.Status,
                        Type = f.Type
                    }).ToListAsync();

                response.Data = flipbooks;
            }
            catch (FormatException)
            {
                response.Success = false;
                response.Message = "El ID de la playlist no tiene un formato válido.";
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Error al obtener los flipbooks por playlist.";
                response.ErrorDetails = ex.Message;
            }

            return response;
        }

        public async Task<ServiceResponse<IEnumerable<string>>> GetFlipbookPagesAsync(string id)
        {
            var response = new ServiceResponse<IEnumerable<string>>();

            try
            {
                var flipbook = await _context.Flipbooks.FindAsync(Guid.Parse(id));

                if (flipbook == null)
                {
                    response.Success = false;
                    response.Message = "Flipbook no encontrado.";
                    return response;
                }

                if (flipbook.Pages == 0 || string.IsNullOrWhiteSpace(flipbook.Directory))
                {
                    response.Success = false;
                    response.Message = "Flipbook sin páginas o sin directorio válido.";
                    return response;
                }

                // Lista las páginas con el formato correcto
                var imageUrls = new List<string>();
                for (int i = 1; i <= flipbook.Pages; i++)
                {
                    // Resultado esperado: /pdfs/{guid}/page_{n}.png
                    string imageUrl = $"{flipbook.Directory}/page_{i}.png";
                    imageUrls.Add(imageUrl);
                }

                response.Data = imageUrls;
                response.Message = "Páginas del flipbook obtenidas exitosamente.";
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Error al obtener las páginas del flipbook.";
                response.ErrorDetails = ex.Message;
            }

            return response;
        }


    }
}
