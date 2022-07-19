using Eventuous;
using Eventuous.AspNetCore.Web;
using Microsoft.AspNetCore.Mvc;
using Savillis.Domain.Bookings;
using Savillis.WebAPI.Application;

namespace Savillis.WebAPI.APIS
{
    // GET
    [Route("/booking")]
    public class BookingAPI : CommandHttpApiBase<Booking> {
        public BookingAPI(IApplicationService<Booking> service) : base(service) { }

        [HttpPost]
        [Route("book")]
        public Task<ActionResult<Result>> BookAppointment(
            [FromBody] BookingCommands.BookAppointment cmd, 
            CancellationToken cancellationToken
        ) => Handle(cmd, cancellationToken);
    }
}