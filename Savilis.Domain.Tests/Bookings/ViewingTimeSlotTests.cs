using Eventuous;
using NodaTime;
using Savillis.Domain.Bookings;

namespace Savilis.Domain.Tests.Bookings;

[TestFixture]
public class ViewingTimeSlotTests
{
    [Test]
    public void ViewingTimeSlot_Should_return_Domain_Exception_Id_StartDate_Is_Greater_Than_EndDate()
    {
        var startHour = new LocalDateTime(2022, 10, 1, 18, 0);
        var endHour = new LocalDateTime(2022, 10, 1, 15, 0);
        Assert.Throws<DomainException>(()=> new ViewingTimeSlot(startHour, endHour));
    }
}