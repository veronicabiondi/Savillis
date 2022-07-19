using System.Threading.Tasks;
using Eventuous.Subscriptions;
using Eventuous.Subscriptions.Context;

namespace Savillis.WebAPI.Infrastructure
{
    public class NotificationsHandler : IEventHandler
    {
        public ValueTask<EventHandlingStatus> HandleEvent(IMessageConsumeContext context)
        {
            throw new System.NotImplementedException();
        }

        public string DiagnosticName { get; }
    }
}