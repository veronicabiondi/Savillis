using NodaTime;

namespace Savillis.Domain.Services
{
    public interface IPropertyCalendarService
    {
        Guid CreateAppointment(string propertyId, LocalDateTime startTime, LocalDateTime endTime);
        IEnumerable<(LocalDateTime, LocalDateTime)> GetAppointments(string propertyId, LocalDate day);
        
    }
}