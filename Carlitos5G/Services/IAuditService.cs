namespace Carlitos5G.Services
{
    public interface IAuditService
    {
        Task LogAsync(string tableName, string action, string recordId, string? changes = null, string? performedBy = null);
    }
}
