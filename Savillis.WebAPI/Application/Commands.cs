using System;

namespace Savillis.WebAPI.Application
{
    public static class BookingCommands
    {
        public record BookAppointment(
            string BookingId,
            string PropertyId,
            string AgentId,
            DateTime From,
            DateTime To,
            DateTime Day
        );
    }
}