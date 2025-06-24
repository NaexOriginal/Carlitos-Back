namespace Carlitos5G.Models
{
    public class AuditLog
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string TableName { get; set; } = null!;
        public string Action { get; set; } = null!; // "Create", "Update", "Delete"
        public string RecordId { get; set; } = null!;
        public string? Changes { get; set; } // JSON string describing changes
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
        public string? PerformedBy { get; set; } // Optional: user email or ID
    }
}
