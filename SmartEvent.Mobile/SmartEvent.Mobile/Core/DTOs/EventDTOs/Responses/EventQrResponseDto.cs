namespace SmartEvent.Mobile.Core.DTOs.EventDTOs.Responses
{
    public class EventQrResponseDto
    {
        public string TokenValue { get; set; } = string.Empty;
        public DateTime ExpiresAt { get; set; }
    }
}
