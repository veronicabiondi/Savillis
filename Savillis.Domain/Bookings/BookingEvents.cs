using System;
using NodaTime;

namespace Savillis.Domain.Bookings.Events {
    public static class BookingEvents {
        public static class V1 {
            public class BookingSubmitted {
                public string BookingId { get; set; }
                public string AgentId { get; set; } 
                public string PropertyId { get; set; }
                public LocalDateTime FromHour { get; set; }
                public LocalDateTime ToHour { get; set; }
                public string SubmittedBy { get; init; }
                public LocalDateTime SubmittedAt { get; init; }
            }
            
            public class BookingCancelled {
                public string BookingId { get; set; }
                public string AgentId { get; set; } 
                public string PropertyId { get; set; }
                public LocalDateTime FromHour { get; set; }
                public LocalDateTime ToHour { get; set; }
                public string SubmittedBy { get; init; }
                public LocalDateTime SubmittedAt { get; init; }
            }
            
            public record BookingDenied(string BookingId, string Reason, LocalDateTime DeniedAt, string UserEmail);
        }
    }
}