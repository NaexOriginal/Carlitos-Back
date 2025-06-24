namespace Carlitos5G.Commons
{
    public class ImageUploadService
    {
        private readonly string _uploadDirectory;

        public ImageUploadService()
        {
            _uploadDirectory = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");

            if (!Directory.Exists(_uploadDirectory))
            {
                Directory.CreateDirectory(_uploadDirectory);
            }
        }

        public async Task<string> UploadImageAsync(IFormFile imageFile)
        {
            if (imageFile == null || imageFile.Length == 0)
            {
                throw new ArgumentException("No se ha seleccionado un archivo para subir.");
            }

            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
            var fileExtension = Path.GetExtension(imageFile.FileName).ToLower();

            if (!allowedExtensions.Contains(fileExtension))
            {
                throw new InvalidOperationException("Tipo de archivo no permitido. Solo se aceptan imágenes JPG, JPEG, PNG o GIF.");
            }

            if (imageFile.Length > 5 * 1024 * 1024) // 5 MB
            {
                throw new InvalidOperationException("El tamaño del archivo excede el límite permitido de 5 MB.");
            }

            var fileName = Guid.NewGuid().ToString() + fileExtension;
            var filePath = Path.Combine(_uploadDirectory, fileName);

            try
            {
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await imageFile.CopyToAsync(stream);
                }
            }
            catch (Exception ex)
            {
                throw new IOException("Error al guardar la imagen en el servidor. Inténtalo de nuevo más tarde. Detalles: " + ex.Message);
            }

            return $"/images/{fileName}";
        }
    }
}
