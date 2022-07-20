using Eventuous;
using NodaTime;

namespace Savillis.Domain.Bookings
{
    public record ViewingTimeSlot
    {
        public LocalDateTime FromHour { get; }
        public LocalDateTime ToHour { get; }

        public ViewingTimeSlot(LocalDateTime fromHour, LocalDateTime toHour)
        {
            if (fromHour > toHour) throw new DomainException("Start date must be before end date");

            (FromHour, ToHour) = (fromHour, toHour);
        }
    }
}