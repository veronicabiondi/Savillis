using NodaTime;

namespace Savillis.Domain.Services
{
    public class PropertyCalendar : IPropertyCalendar
    {
        public Guid CreateAppointment(string propertyId, LocalDateTime startTime, LocalDateTime endTime)
        {
            //Assume Successful creation
            return Guid.NewGuid();
        }

        public IEnumerable<(LocalDateTime, LocalDateTime)> GetAppointments(string propertyId, LocalDateTime day)
        {
            throw new NotImplementedException();
        }
    }
}