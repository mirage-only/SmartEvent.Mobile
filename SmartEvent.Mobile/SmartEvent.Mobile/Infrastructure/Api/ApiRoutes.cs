namespace SmartEvent.Mobile.Infrastructure.Api;

public static class ApiRoutes
{
    public const string BaseUrl = "http://192.168.1.12:5187";
    
    private const string UserUrl = $"{BaseUrl}/users";
    
    private const string EventUrl = $"{BaseUrl}/events";
    public static readonly string GetLightEvents = $"{EventUrl}/getLightEventsWithPagination";
    public static readonly string GetEventDetails = $"{EventUrl}/get";
    
    
    private const string AuthUrl = $"{BaseUrl}/users/auth";
    public static readonly string RegisterUrl = $"{AuthUrl}/register";
    public static readonly string LoginUrl = $"{AuthUrl}/login";
    
    private const string RegistrationUrl =  $"{BaseUrl}/registration";
    public static readonly string EventRegistrationUrl = $"{RegistrationUrl}/regForEvent";
    public static readonly string IsRegistrationExistUrl =  $"{RegistrationUrl}/isRegistrationExist";
    
}