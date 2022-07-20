using Eventuous;
using Savillis.Domain.Bookings.Events;

namespace Savillis.Domain.Bookings
{
    public record BookingState : AggregateState<BookingState,BookingId>
    {
        public string AgentId { get; set; }
        public string PropertyId { get; set; }
        public BookingState() {
            On<BookingEvents.V1.BookingSubmitted>(HandleSubmittedBooking);
        }

        static BookingState HandleSubmittedBooking(BookingState state, BookingEvents.V1.BookingSubmitted e)
            => state with {
                Id = new BookingId(e.BookingId),
                AgentId = e.AgentId,
                PropertyId = e.PropertyId,
                
            };
    }
}