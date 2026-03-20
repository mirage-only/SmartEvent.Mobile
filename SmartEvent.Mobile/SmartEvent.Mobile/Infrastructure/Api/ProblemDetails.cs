namespace SmartEvent.Mobile.Infrastructure.Api
{
    public class ProblemDetails
    {
        public int Status { get; set; }
        public string? Title { get; set; }
        public string? Detail { get; set; }
        public Dictionary<string, string[]>? Errors { get; set; }
        public string? Instance { get; set; }
    }

}
