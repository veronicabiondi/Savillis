using Eventuous;
using Moq;
using NodaTime;
using Savillis.Domain.Bookings;
using Savillis.Domain.DomainService;
using Savillis.Library.Result;

namespace Savilis.Domain.Tests.Bookings;

[TestFixture]
public class BookingTests
{
    private readonly Mock<IDomainService> _domainService;

    public BookingTests() =>  _domainService = new Mock<IDomainService>();

    [Test]
    public async Task BookAppointment_Should_Return_DomainException_If_Booking_Is_Longer_Than_Two_Hours()
    {
        var booking = new Booking();

        _domainService.Setup(x => x.TryBookOnAgentsCalendar(It.IsAny<string>(), It.IsAny<ViewingTimeSlot>())).Returns(new SuccessfulResult<Guid>(new Guid()));
        _domainService.Setup(x => x.TryBookOnPropertyCalendar(It.IsAny<string>(), It.IsAny<ViewingTimeSlot>(),It.IsAny<LocalDate>())).Returns(new SuccessfulResult<Guid>(new Guid()));

        var bookingId = new BookingId(Guid.NewGuid().ToString());
        var appointment =
            new ViewingTimeSlot(new LocalDateTime(2022, 10, 12, 12, 0), new LocalDateTime(2022, 10, 12, 15, 0));
        
       Assert.Throws<DomainException>(()=>booking.BookAppointment(bookingId, "PP-Agent", "Epping-AASD", appointment,
           _domainService.Object.TryBookOnPropertyCalendar, _domainService.Object.TryBookOnAgentsCalendar,
           new LocalDate()));
    }
}