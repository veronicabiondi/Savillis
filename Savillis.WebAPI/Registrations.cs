using System.Text.Json;
using Eventuous;
using Eventuous.EventStore;
using Eventuous.EventStore.Subscriptions;
using Eventuous.Projections.MongoDB;
using Eventuous.Subscriptions.Registrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NodaTime;
using NodaTime.Serialization.SystemTextJson;
using Savillis.Domain.Bookings;
using Savillis.WebAPI.Application;
using Savillis.WebAPI.Application.Queries;
using Savillis.WebAPI.Infrastructure;

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
            
            services.AddSingleton(Mongo.ConfigureMongo(configuration));
            services.AddCheckpointStore<MongoCheckpointStore>();

            services.AddSubscription<AllStreamSubscription, AllStreamSubscriptionOptions>(
                "BookingsProjections",
                builder => builder
                    .Configure(cfg => cfg.ConcurrencyLimit = 2)
                    .UseCheckpointStore<MongoCheckpointStore>()
                    .AddEventHandler<BookingStateProjection>()
                    .WithPartitioningByStream(2));
        }
    }
}