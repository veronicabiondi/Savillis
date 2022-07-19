using Eventuous;

namespace Savillis.Domain.Bookings
{
    public record BookingId : AggregateId {
        public BookingId(string id) : base(id) { }
    }
}