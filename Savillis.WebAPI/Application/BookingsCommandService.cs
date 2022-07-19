using Eventuous;
using NodaTime;
using Savillis.Domain.Bookings;
using Savillis.Domain.DomainService;

namespace Savillis.WebAPI.Application
{
    public class BookingsCommandService : ApplicationService<Booking, BookingState, BookingId> {
        public BookingsCommandService(IAggregateStore store, IDomainService service) : base(store)
        {
            OnNew<BookingCommands.BookAppointment>(
                (booking, cmd) => booking.BookAppointment(
                    new BookingId(cmd.BookingId),
                    cmd.AgentId,
                    cmd.PropertyId,
                    new ViewingTimeSlot(LocalDateTime.FromDateTime(cmd.From), LocalDateTime.FromDateTime(cmd.To)), 
                    service.TryBookOnPropertyCalendar,
                    service.TryBookOnAgentsCalendar,
                    LocalDate.FromDateTime(cmd.Day)
                )
            );
        }
    }
}