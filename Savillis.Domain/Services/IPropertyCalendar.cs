using NodaTime;

namespace Savillis.Domain.Services
{
    public interface IPropertyCalendar
    {
        Guid CreateAppointment(string propertyId, LocalDateTime startTime, LocalDateTime endTime);
        IEnumerable<(LocalDateTime, LocalDateTime)> GetAppointments(string propertyId, LocalDateTime day);
    }
}