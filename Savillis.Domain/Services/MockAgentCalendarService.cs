using System.Collections.Concurrent;
using Eventuous;
using NodaTime;

namespace Savillis.Domain.Services
{
    public class MockAgentCalendarService : IAgentCalendarService
    {
        private static readonly ConcurrentDictionary<(LocalDateTime, LocalDateTime), bool> _dbSet = new();

        public MockAgentCalendarService()
        {
            _dbSet.TryAdd((new LocalDateTime(2022, 10, 3, 8, 0), new LocalDateTime(2022, 10, 3, 10, 0)), false);
            _dbSet.TryAdd((new LocalDateTime(2022, 10, 3, 12, 4), new LocalDateTime(2022, 10, 3, 14, 0)), false);
            _dbSet.TryAdd((new LocalDateTime(2022, 10, 3, 16, 4), new LocalDateTime(2022, 10, 3, 18, 0)), false);
        }

        public Guid CreateAppointment(string agentId, LocalDateTime startTime, LocalDateTime endTime)
        {
            var result = _dbSet.TryAdd((startTime, endTime), true);
            //Assume Successful creation
            return result ? Guid.NewGuid() : throw new DomainException("Failed To Create an appointment");
        }

        public IEnumerable<(LocalDateTime, LocalDateTime)> GetAppointments(string agentId, LocalDateTime startTime,
            LocalDateTime endTime)
        {
            var dates = _dbSet.Where(x=> x.Key.Item1 >= startTime && x.Key.Item2<= endTime && x.Value == false )
                .Select(x => (x.Key.Item1, x.Key.Item2)).ToList();
            
            return dates.Any() ? dates : Enumerable.Empty<(LocalDateTime, LocalDateTime)>();
        }
    }
}
