using Carlitos5G.Data;
using Carlitos5G.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Carlitos5G.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuditLogsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AuditLogsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/AuditLogs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AuditLog>>> GetAuditLogs()
        {
            var logs = await _context.AuditLogs
                                     .ToListAsync();

            return Ok(logs);
        }

        // GET: api/AuditLogs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AuditLog>> GetAuditLog(string id)
        {
            if (!Guid.TryParse(id, out Guid auditLogGuidId))
            {
                return BadRequest(new
                {
                    Success = false,
                    Message = "ID de administrador inválido",
                    ErrorDetails = "El formato del ID proporcionado no es un GUID válido"
                });
            }
            
            var log = await _context.AuditLogs.FindAsync(auditLogGuidId);

            if (log == null)
            {
                return NotFound();
            }

            return Ok(log);
        }
    }
}
