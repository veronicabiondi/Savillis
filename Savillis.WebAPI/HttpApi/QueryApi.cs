using Eventuous;
using Microsoft.AspNetCore.Mvc;
using Savillis.Domain.Bookings;

namespace Savillis.WebAPI.APIS
{
    
    [Route("/bookings")]
    public class QueryApi : ControllerBase {
        readonly IAggregateStore _store;
        
        public QueryApi(IAggregateStore store) => _store = store;

        // We could also load from Mongo, if we subscribe to the MongoProjection service
        [HttpGet]
        [Route("{id}")]
        public async Task<BookingState> GetBooking(string id, CancellationToken cancellationToken) {
            var booking = await _store.Load<Booking>(new BookingId(id), cancellationToken);
            return booking.State;
        }
    }
}