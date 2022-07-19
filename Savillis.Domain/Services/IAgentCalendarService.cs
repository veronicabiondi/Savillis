using NodaTime;

namespace Savillis.Domain.Services
{
    public interface IAgentCalendarService
    {
        Guid CreateAppointment(string agentId, LocalDateTime startTime, LocalDateTime endTime);
        IEnumerable<(LocalDateTime, LocalDateTime)> GetAppointments(string agentId, LocalDateTime startTime, LocalDateTime endTime);
    }
}