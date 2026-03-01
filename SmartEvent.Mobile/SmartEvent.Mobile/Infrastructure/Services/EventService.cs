using SmartEvent.Mobile.Core.DTOs.EventDTOs.Responses;
using SmartEvent.Mobile.Core.IServices;
using SmartEvent.Mobile.Infrastructure.Api;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartEvent.Mobile.Infrastructure.Services
{
    public class EventService : IEventService
    {
        private readonly IApiClient _apiClient;

        public EventService(IApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        public async Task<List<EventLightDto>> GetAllEvents()
        {
            //var result = await _apiClient.GetAsync<List<EventLightDto>>("/events"); 
            //if (!result.IsSuccess) 
            //    throw new Exception(result.Error);
            //return result.Data ?? new List<EventLightDto>();
            var list = new List<EventLightDto> 
            { 
                new EventLightDto 
                { 
                    Id = Guid.NewGuid(), 
                    Name = "Тестовое мероприятие 1", 
                    Description = "Описание тестового мероприятия", 
                    StartTime = DateTime.Now.AddDays(1) 
                }, 
                new EventLightDto 
                { 
                    Id = Guid.NewGuid(),
                    Name = "Тестовое мероприятие 2", 
                    Description = "Ещё одно тестовое событие", 
                    StartTime = DateTime.Now.AddDays(2)
                }
            };
            return list;
        }
    }
}
