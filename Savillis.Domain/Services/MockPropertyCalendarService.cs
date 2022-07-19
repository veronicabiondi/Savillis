using System.Collections.Concurrent;
using Eventuous;
using NodaTime;

namespace Savillis.Domain.Services
{
    
    //We assume that TryAdd is thread-safe. It is possible for one thread to retrieve a value and another to immediately update..We can use locks for this but the test will never finish :)))))
    public class MockPropertyCalendarService : IPropertyCalendarService
    {
        private static readonly ConcurrentDictionary<(LocalDateTime, LocalDateTime), bool> _dbSet = new();

        public MockPropertyCalendarService()
        {
            _dbSet.TryAdd((new LocalDateTime(2022, 10, 3, 8, 0), new LocalDateTime(2022, 10, 3, 10, 0)), false);
            _dbSet.TryAdd((new LocalDateTime(2022, 10, 3, 12, 4), new LocalDateTime(2022, 10, 3, 14, 0)), false);
            _dbSet.TryAdd((new LocalDateTime(2022, 10, 3, 16, 4), new LocalDateTime(2022, 10, 3, 18, 0)), false);
        }

        public Guid CreateAppointment(string propertyId, LocalDateTime startTime, LocalDateTime endTime)
        {
            var result = _dbSet.TryAdd((startTime, endTime), true);
            //Assume Successful creation
            return result ? Guid.NewGuid() : throw new DomainException("Failed To Create an appointment");
        }

        //Return available appointments for that date
        public IEnumerable<(LocalDateTime, LocalDateTime)> GetAppointments(string propertyId, LocalDate day)
        {
            var dates = _dbSet.Where(x => x.Key.Item1.Date == day && x.Value == false)
                .Select(x => (x.Key.Item1, x.Key.Item2));

            return dates.Any() ? dates : Enumerable.Empty<(LocalDateTime, LocalDateTime)>();
        }
    }
}