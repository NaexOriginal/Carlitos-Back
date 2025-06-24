namespace Carlitos5G.Commons
{
    public class ServiceResponse<T>
    {
        public bool Success { get; set; } = true;
        public string? Message { get; set; }
        public T? Data { get; set; }
        public string? ErrorDetails { get; set; }
    }
}
