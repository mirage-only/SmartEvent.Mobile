namespace SmartEvent.Mobile.Core.DTOs.EventDTOs.Responses
{
    public class EventDetailsDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public DateTime StartTime { get; set; } = DateTime.UtcNow;
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public Guid CreatorId { get; set; }
    }
}
