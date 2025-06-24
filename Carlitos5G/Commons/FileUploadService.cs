using System;
using System.IO;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;
using Microsoft.AspNetCore.Http;
using PdfiumViewer;

namespace Carlitos5G.Commons
{
    public class FileUploadService
    {
        private readonly string _uploadDirectory;

        public FileUploadService()
        {
            // Configura el directorio donde se almacenarán los archivos PDF y las imágenes
            _uploadDirectory = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "pdfs");

            // Asegúrate de que el directorio exista
            if (!Directory.Exists(_uploadDirectory))
            {
                Directory.CreateDirectory(_uploadDirectory);
            }
        }

        public async Task<PdfUploadResult> UploadAndConvertPdfAsync(IFormFile pdfFile)
        {
            if (pdfFile == null || pdfFile.Length == 0)
            {
                throw new ArgumentException("No file selected for upload");
            }

            var allowedExtension = ".pdf";
            var fileExtension = Path.GetExtension(pdfFile.FileName).ToLower();
            if (fileExtension != allowedExtension)
            {
                throw new InvalidOperationException("Invalid file type. Only PDF files are allowed.");
            }

            if (pdfFile.Length > 10 * 1024 * 1024)
            {
                throw new InvalidOperationException("File size exceeds the 10 MB limit.");
            }

            var fileName = Guid.NewGuid().ToString() + fileExtension;
            var filePath = Path.Combine(_uploadDirectory, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await pdfFile.CopyToAsync(stream);
            }

            var imagesDirectory = Path.Combine(_uploadDirectory, Path.GetFileNameWithoutExtension(fileName));
            Directory.CreateDirectory(imagesDirectory);

            // Nueva lógica: devuelve la cantidad de páginas al convertir
            int pageCount = ConvertPdfToImages(filePath, imagesDirectory);

            return new PdfUploadResult
            {
                PdfUrl = $"/pdfs/{fileName}",
                PageCount = pageCount
            };
        }


        private int ConvertPdfToImages(string pdfFilePath, string imagesDirectory)
        {
            using (var pdfDocument = PdfDocument.Load(pdfFilePath))
            {
                int pageCount = pdfDocument.PageCount;

                for (int i = 0; i < pageCount; i++)
                {
                    using (var image = pdfDocument.Render(i, 300, 300, true))
                    {
                        var imageFilePath = Path.Combine(imagesDirectory, $"page_{i + 1}.png");
                        image.Save(imageFilePath, ImageFormat.Png);
                    }
                }

                return pageCount;
            }
        }

    }

    public class PdfUploadResult
    {
        public string PdfUrl { get; set; } = null!;
        public int PageCount { get; set; }
    }
}