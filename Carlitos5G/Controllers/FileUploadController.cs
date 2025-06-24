using Microsoft.AspNetCore.Mvc;
using Carlitos5G.Commons;

namespace Carlitos5G.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileUploadController : ControllerBase
    {
        private readonly FileUploadService _pdfUploadService;

        public FileUploadController()
        {
            _pdfUploadService = new FileUploadService();
        }

        [HttpPost("upload-pdf")]
        public async Task<IActionResult> UploadPdf(IFormFile pdfFile)
        {
            try
            {
                var fileUrl = await _pdfUploadService.UploadAndConvertPdfAsync(pdfFile);
                return Ok(new { Url = fileUrl });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }
    }
}
