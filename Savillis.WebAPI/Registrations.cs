using System.Text.Json;
using Eventuous;
using Eventuous.EventStore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NodaTime;
using NodaTime.Serialization.SystemTextJson;
using Savillis.Domain.Bookings;
using Savillis.WebAPI.Application;

namespace Savillis.WebAPI
{
    public static class Registrations
    {
        public static void AddEventuous(this IServiceCollection services, IConfiguration configuration)
        {
            DefaultEventSerializer.SetDefaultSerializer(
                new DefaultEventSerializer(
                    new JsonSerializerOptions(JsonSerializerDefaults.Web).ConfigureForNodaTime(DateTimeZoneProviders
                        .Tzdb)
                )
            );

            services.AddEventStoreClient(configuration["EventStore:ConnectionString"]);
            services.AddAggregateStore<EsdbEventStore>();
            services.AddApplicationService<BookingsCommandService, Booking>();
        }
    }
}