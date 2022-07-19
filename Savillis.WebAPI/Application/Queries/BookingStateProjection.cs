using Eventuous.Projections.MongoDB;
using MongoDB.Driver;
using Savillis.Domain.Bookings.Events;

namespace Savillis.WebAPI.Application.Queries;

public class BookingStateProjection : MongoProjection<BookingDocument> {
    public BookingStateProjection(IMongoDatabase database) : base(database) {
        On<BookingEvents.V1.BookingSubmitted>(evt => evt.BookingId, HandleBookingSubmitted);
    }

    static UpdateDefinition<BookingDocument> HandleBookingSubmitted(
        BookingEvents.V1.BookingSubmitted evt, UpdateDefinitionBuilder<BookingDocument> update
    )
        => update.SetOnInsert(x => x.Id, evt.BookingId)
            .Set(x => x.AgentId, evt.AgentId)
            .Set(x => x.PropertyId, evt.PropertyId)
            .Set(x => x.Date, evt.FromHour.Date);
}