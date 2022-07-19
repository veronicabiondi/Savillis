using System.Collections.Concurrent;
using Eventuous;
using NodaTime;

namespace Savillis.Domain.Services
{
    
    //We assume that TryAdd is thread-safe. It is possible for one thread to retrieve a value and another to immediately update..We can use locks for this but the test will never finish :)))))
    public class MockPropertyCalendarService : IPropertyCalendarService
    {
        private static ConcurrentDictionary<(LocalDateTime, LocalDateTime),bool> AvailableDates =
            new ConcurrentDictionary<(LocalDateTime, LocalDateTime), bool>
            {
              //  {KeyValuePair.Create((new LocalDateTime(2022, 10, 1, 13, 1), new LocalDateTime(2022, 10, 1, 15, 1)), false)

            };
            
        public Guid CreateAppointment(string agentId, LocalDateTime startTime, LocalDateTime endTime)
        {
            var result = AvailableDates.TryAdd((startTime, endTime), true);
            //Assume Successful creation
            return result ? Guid.NewGuid() : throw new DomainException("Failed To Create an appointment");
        }

        //Return available appointments for that date
        public IEnumerable<(LocalDateTime, LocalDateTime)> GetAppointments(string propertyId, LocalDate day)
        {
            var dates = AvailableDates.Where(x => x.Key.Item1.Date == day && x.Value == false)
                .Select(x => (x.Key.Item1, x.Key.Item2));

            return dates.Any() ? dates : Enumerable.Empty<(LocalDateTime, LocalDateTime)>();
        }
    }
}