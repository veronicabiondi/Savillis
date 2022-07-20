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
    private Mock<IDomainService> _domainService;
    private Booking _booking = new Booking();
    private BookingId _bookingId = new BookingId(Guid.NewGuid().ToString());
    
    [SetUp]
    public void Init() =>  _domainService = new Mock<IDomainService>();

    [Test]
    public void BookAppointment_Should_Return_DomainException_If_Booking_Is_Longer_Than_Two_Hours()
    {
        _domainService.Setup(x => x.TryBookOnAgentsCalendar(It.IsAny<string>(), It.IsAny<ViewingTimeSlot>())).Returns(new SuccessfulResult<Guid>(new Guid()));
        _domainService.Setup(x => x.TryBookOnPropertyCalendar(It.IsAny<string>(), It.IsAny<ViewingTimeSlot>(),It.IsAny<LocalDate>())).Returns(new SuccessfulResult<Guid>(new Guid()));
        
        var appointment =
            new ViewingTimeSlot(new LocalDateTime(2022, 10, 12, 12, 0), new LocalDateTime(2022, 10, 12, 15, 0));
        
        AssertExceptionIsThrown(appointment);
    }
    
    [Test]
    public void BookAppointment_Should_Return_DomainException_If_Booking_Is_After_1800()
    {
        _domainService.Setup(x => x.TryBookOnAgentsCalendar(It.IsAny<string>(), It.IsAny<ViewingTimeSlot>())).Returns(new SuccessfulResult<Guid>(new Guid()));
        _domainService.Setup(x => x.TryBookOnPropertyCalendar(It.IsAny<string>(), It.IsAny<ViewingTimeSlot>(),It.IsAny<LocalDate>())).Returns(new SuccessfulResult<Guid>(new Guid()));

        var appointment =
            new ViewingTimeSlot(new LocalDateTime(2022, 10, 12, 18, 0), new LocalDateTime(2022, 10, 12, 20, 0));
        
        AssertExceptionIsThrown(appointment);
    }
    
    [Test]
    public void BookAppointment_Should_Return_DomainException_If_Booking_Is_Before_0800()
    {
         _domainService.Setup(x => x.TryBookOnAgentsCalendar(It.IsAny<string>(), It.IsAny<ViewingTimeSlot>())).Returns(new SuccessfulResult<Guid>(new Guid()));
        _domainService.Setup(x => x.TryBookOnPropertyCalendar(It.IsAny<string>(), It.IsAny<ViewingTimeSlot>(),It.IsAny<LocalDate>())).Returns(new SuccessfulResult<Guid>(new Guid()));
        
        var appointment =
            new ViewingTimeSlot(new LocalDateTime(2022, 10, 12, 6, 0), new LocalDateTime(2022, 10, 12, 8, 0));

        AssertExceptionIsThrown(appointment);
    }

    private void AssertExceptionIsThrown(ViewingTimeSlot appointment)
    {
        Assert.Throws<DomainException>(()=> _booking.BookAppointment(_bookingId, "PP-Agent", "Epping-AASD", appointment,
            _domainService.Object.TryBookOnPropertyCalendar, _domainService.Object.TryBookOnAgentsCalendar,
            new LocalDate()));
    }
}