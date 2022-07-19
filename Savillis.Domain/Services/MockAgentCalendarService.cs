using NodaTime;

namespace Savillis.Domain.Services
{
    
    public class MockAgentCalendarService : IAgentCalendarService
    {
        private static List<(LocalDateTime, LocalDateTime, string)> AvailableDates =
            new List<(LocalDateTime, LocalDateTime, string)>
            {
                (new LocalDateTime(2022, 10, 1, 0, 1), new LocalDateTime(2022, 10, 1, 12, 1), "Agent-A"),
                (new LocalDateTime(2022, 10, 1, 13, 1), new LocalDateTime(2022, 10, 1, 15, 1), "Agent-B"),
                (new LocalDateTime(2022, 10, 1, 16, 1), new LocalDateTime(2022, 10, 24, 23, 1), "Agent-C")
            };
            
        public Guid CreateAppointment(string agentId, LocalDateTime startTime, LocalDateTime endTime)
        {
            AvailableDates.Add((startTime, endTime, agentId));
            //Assume Successful creation
            return Guid.NewGuid();
        }

        public IEnumerable<(LocalDateTime, LocalDateTime)> GetAppointments(string agentId, LocalDateTime startTime, LocalDateTime endTime) => 
            AvailableDates.Select(x => (x.Item1, x.Item2));
    }
}
