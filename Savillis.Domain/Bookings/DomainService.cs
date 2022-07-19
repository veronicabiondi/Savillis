using System;
using System.Linq;
using NodaTime;
using Savillis.Domain.Bookings;
using Savillis.Domain.Services;
using Savillis.Library.Result;

namespace Savillis.Domain.DomainService
{
    public interface IDomainService
    {
        BaseResult<Guid> TryBookOnPropertyCalendar(string propertyId, ViewingTimeSlot timeslot, LocalDateTime end);
        BaseResult<Guid> TryBookOnAgentsCalendar(string agentId, ViewingTimeSlot timeslot);
    }

    public class DomainService : IDomainService
    {
        private readonly IAgentCalendar _agentCalendar;
        private readonly IPropertyCalendar _propertyCalendar;

        public DomainService(IAgentCalendar agentCalendar, IPropertyCalendar propertyCalendar)
        {
            _agentCalendar = agentCalendar;
            _propertyCalendar = propertyCalendar;
        }

        //We could also merge the below methods in one - TryBookOnCalendars, so that the Booking Aggregate Method does not need more than one Func domain service
        public BaseResult<Guid> TryBookOnPropertyCalendar(string propertyId, ViewingTimeSlot timeslot, LocalDateTime day)
        {
            Guid? guid = new Guid();
            try
            {
                var availableAppointments = _propertyCalendar.GetAppointments(propertyId, day);
                
                if (!availableAppointments.Any())
                    return new FailureResult<Guid>(new ErrorMessage(){ Message = "No Available appointments"});
                    
                //TODO Check to see if your appointment  falls within the available appointments
                guid = _propertyCalendar.CreateAppointment(propertyId, timeslot.FromHour, timeslot.ToHour);
                
                //In case there is no exception but internally they system failed to reserve an appointment
                if(guid == null)
                    return new FailureResult<Guid>(new ErrorMessage(){ Message = "Service failed to book an appointment"});
                
                return new SuccessfulResult<Guid>(guid.GetValueOrDefault());
            }
            catch (Exception ex)
            {
                return new FailureResult<Guid>(new ErrorMessage());
            }
        }
        
        public BaseResult<Guid> TryBookOnAgentsCalendar(string agentId, ViewingTimeSlot timeslot)
        {
            Guid? guid = new Guid();
            try
            {
                var availableAppointments = _agentCalendar.GetAppointments(agentId, timeslot.FromHour, timeslot.ToHour);
                
                if (!availableAppointments.Any())
                    return new FailureResult<Guid>(new ErrorMessage(){ Message = "No Available appointments"});
                
                guid = _agentCalendar.CreateAppointment(agentId, timeslot.FromHour, timeslot.ToHour);
                
                return new SuccessfulResult<Guid>(guid.GetValueOrDefault());
            }
            catch (Exception e)
            {
                return new FailureResult<Guid>(new ErrorMessage());
            }
           
        }
    }
}