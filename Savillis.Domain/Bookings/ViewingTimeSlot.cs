using Eventuous;
using NodaTime;

namespace Savillis.Domain.Bookings
{
    public record ViewingTimeSlot
    {
        public LocalDateTime FromHour { get; }
        public LocalDateTime ToHour { get; }

        public ViewingTimeSlot()
        {
        }

        public ViewingTimeSlot(LocalDateTime fromHour, LocalDateTime toHour)
        {
            if (FromHour.Date > toHour.Date) throw new DomainException("Check in date must be before check out date");

            (FromHour, toHour) = (FromHour, toHour);
        }
    }
}