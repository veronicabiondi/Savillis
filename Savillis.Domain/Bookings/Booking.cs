using System;
using System.Security.Cryptography;
using Eventuous;
using NodaTime;
using Savillis.Domain.Bookings.Events;
using Savillis.Library.Result;

namespace Savillis.Domain.Bookings
{
    public class Booking : Aggregate<BookingState, BookingId> {

        public void BookAppointment(BookingId bookingId, string  agentId, string propertyId, ViewingTimeSlot timeslot, 
            Func<string,ViewingTimeSlot,LocalDateTime,BaseResult> propertyFunc, 
            Func<string,ViewingTimeSlot,BaseResult> agentFunc, LocalDateTime day)
        {
            //Inspect Result
            var propertyServiceResult = propertyFunc(propertyId, timeslot, day);
            var agentServiceResult = agentFunc(agentId, timeslot);

            if (!propertyServiceResult.IsSuccessful)
                throw new DomainException(propertyServiceResult.Error.Message);
            
            if (!agentServiceResult.IsSuccessful)
                throw new DomainException(agentServiceResult.Error.Message);

            var period = timeslot.ToHour - timeslot.FromHour;
            
            if(period.Hours > 2)
                throw new DomainException("Booking can't be longer than 2 hours");

            if(timeslot.FromHour.TimeOfDay.Hour < 8)
                throw new DomainException("Booking can't be longer than 2 hours");
            
            if(timeslot.ToHour.TimeOfDay.Hour > 18)
                throw new DomainException("Booking can't be longer than 2 hours");
            
            Apply(
                new BookingEvents.V1.BookingSubmitted() {
                    BookingId = bookingId,
                    AgentId = agentId,
                    PropertyId = propertyId,
                    FromHour = timeslot.FromHour,
                    ToHour = timeslot.ToHour,
                    SubmittedBy = "User"
                 }
            );
        }
    }
}