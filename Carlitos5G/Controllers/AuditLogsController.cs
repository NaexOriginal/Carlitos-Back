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
        public async Task<ActionResult<AuditLog>> GetAuditLog(int id)
        {
            var log = await _context.AuditLogs.FindAsync(id);

            if (log == null)
            {
                return NotFound();
            }

            return Ok(log);
        }
    }
}
