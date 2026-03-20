namespace SmartEvent.Mobile.Core.DTOs.EventDTOs.Requests;

public class AddEventDto
{
    public string Name { get; set; } =  string.Empty;
    public string Description { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
    public DateTime StartTime { get; set; }
    public double Latitude { get; set;}
    public double Longitude { get; set;}
    public string Address { get; set; } = string.Empty;
    public string Room { get; set; } = string.Empty;
    public uint QrCodeExpirationTime { get; set;}

}