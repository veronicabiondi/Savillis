using System.Threading.Tasks;
using Eventuous.Subscriptions;
using Eventuous.Subscriptions.Context;
using Savillis.Domain.Bookings.Events;


namespace Savillis.WebAPI.APIS
{
    public class IntegrationCalendarHandler : IEventHandler
    {
        public ValueTask<EventHandlingStatus> HandleEvent(IMessageConsumeContext context)
        {
            return context.Message switch {
                BookingEvents.V1.BookingSubmitted approved => HandleBookingSubmitted(approved),
                
                _ => new ValueTask<EventHandlingStatus>(EventHandlingStatus.Success)
            };

            async ValueTask<EventHandlingStatus> HandleBookingSubmitted(BookingEvents.V1.BookingSubmitted paymentApproved)
            {
                //Integration with the calendar service
                return EventHandlingStatus.Success;
            }
        }

        public string DiagnosticName { get; }
    }
}