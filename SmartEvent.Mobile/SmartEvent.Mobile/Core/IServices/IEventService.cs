using SmartEvent.Mobile.Core.DTOs.EventDTOs.Responses;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartEvent.Mobile.Core.IServices
{
    public interface IEventService
    {
        Task<List<EventLightDto>> GetAllEvents();
    }
}
