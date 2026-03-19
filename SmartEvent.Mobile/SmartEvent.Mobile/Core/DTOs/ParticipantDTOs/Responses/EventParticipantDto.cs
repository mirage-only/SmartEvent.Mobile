using System;
using System.Collections.Generic;
using System.Text;

namespace SmartEvent.Mobile.Core.DTOs.ParticipantDTOs.Responses
{
    public class EventParticipantDto
    {
        public Guid UserId { get; set; }
        public string FullName { get; set; } = "";
        public bool IsRegistered { get; set; }
        public bool IsAttended { get; set; }
    }

}
