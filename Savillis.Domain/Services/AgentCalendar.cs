using System;
using System.Collections.Generic;
using NodaTime;

namespace Savillis.Domain.Services
{
    public class AgentCalendar : IAgentCalendar
    {
        public Guid CreateAppointment(string agentId, LocalDateTime startTime, LocalDateTime endTime)
        {
            //Assume Successful creation
            return Guid.NewGuid();
        }

        public IEnumerable<(LocalDateTime, LocalDateTime)> GetAppointments(string agentId, LocalDateTime startTime, LocalDateTime endTime)
        {
            throw new NotImplementedException();
        }
    }
}