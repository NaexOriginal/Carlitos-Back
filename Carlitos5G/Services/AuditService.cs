using Carlitos5G.Data;
using Carlitos5G.Models;

namespace Carlitos5G.Services
{
    public class AuditService : IAuditService
    {
        private readonly ApplicationDbContext _context;

        public AuditService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task LogAsync(string tableName, string action, string recordId, string? changes = null, string? performedBy = null)
        {
            var log = new AuditLog
            {
                TableName = tableName,
                Action = action,
                RecordId = recordId,
                Changes = changes,
                PerformedBy = performedBy,
                Timestamp = DateTime.UtcNow
            };

            _context.AuditLogs.Add(log);
            await _context.SaveChangesAsync();
        }
    }
}
