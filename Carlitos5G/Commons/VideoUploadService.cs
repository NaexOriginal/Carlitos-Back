using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Carlitos5G.Commons
{
    public class VideoUploadService
    {
        private readonly string _uploadDirectory;

        public VideoUploadService()
        {
            _uploadDirectory = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "videos");

            if (!Directory.Exists(_uploadDirectory))
            {
                Directory.CreateDirectory(_uploadDirectory);
            }
        }

        public async Task<string> UploadVideoAsync(IFormFile videoFile)
        {
            if (videoFile == null || videoFile.Length == 0)
                throw new ArgumentException("No se ha seleccionado un archivo para subir.");

            var allowedExtensions = new[] { ".mp4", ".mov", ".avi", ".webm", ".mkv" };
            var fileExtension = Path.GetExtension(videoFile.FileName).ToLower();

            if (!allowedExtensions.Contains(fileExtension))
                throw new InvalidOperationException("Tipo de archivo no permitido. Solo se aceptan videos MP4, MOV, AVI, WEBM o MKV.");

            if (videoFile.Length > 200 * 1024 * 1024) // 200 MB
                throw new InvalidOperationException("El tamaño del archivo excede el límite permitido de 200 MB.");

            var fileName = Guid.NewGuid() + fileExtension;
            var filePath = Path.Combine(_uploadDirectory, fileName);

            try
            {
                using var stream = new FileStream(filePath, FileMode.Create);
                await videoFile.CopyToAsync(stream);
            }
            catch (Exception ex)
            {
                throw new IOException("Error al guardar el video. Inténtalo de nuevo. Detalles: " + ex.Message);
            }

            return $"/videos/{fileName}";
        }
    }
}
