using NodaTime;
using Savillis.Domain.Services;
using static NUnit.Framework.Assert;

namespace Savilis.Domain.Tests.Services;

[TestFixture]
public class MockPropertyCalendarServiceTests
{
    private MockPropertyCalendarService _service = new();

    [Test]
    public void CreateAppointment_Should_Add_An_Appointment_To_The_Collection()
    {
        //ARRANGE
        var startLocalDateHour= new LocalDateTime(2022, 10, 1, 16, 0);
        var endLocalDateHour= new LocalDateTime(2022, 10, 1, 18, 0);
        //ACT
        var guid = _service.CreateAppointment("Purple-Bricks-Agent", startLocalDateHour, endLocalDateHour);
        //ASSERT
        NotNull(guid);
    }

    [Test]
    public void GetGetAppointments_Should_Return_Available_Appointments()
    {
        //ARRANGE
        var startLocalDateHour= new LocalDateTime(2022, 10, 1, 16, 0);
        var endLocalDateHour= new LocalDateTime(2022, 10, 1, 18, 0);
        //ACT
        var availableAppointments = _service.GetAppointments("Purple-Bricks-Agent", new LocalDate(2022, 10, 1));
        //ASSERT
        That(availableAppointments.Any());
    }
    
    [Test]
    public void GetGetAppointments_Should_Return_Return_Empty_List_If_Non_AvailableAppointmens()
    {
        //ARRANGE
        var startLocalDateHour= new LocalDateTime(2022, 10, 1, 16, 0);
        var endLocalDateHour= new LocalDateTime(2022, 10, 1, 18, 0);
        //ACT
        var availableAppointments = _service.GetAppointments("Purple-Bricks-Agent", new LocalDate(2022, 10, 2));
        //ASSERT
        IsEmpty(availableAppointments);
    }
}