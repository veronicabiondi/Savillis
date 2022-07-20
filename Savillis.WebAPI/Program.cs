using Eventuous;
using Eventuous.AspNetCore;
using Eventuous.Diagnostics.Logging;
using Microsoft.OpenApi.Models;
using NodaTime;
using NodaTime.Serialization.SystemTextJson;
using Savillis.Domain.Bookings.Events;
using Savillis.Domain.DomainService;
using Savillis.Domain.Services;
using Savillis.WebAPI;
using Serilog;
using Serilog.Events;

TypeMap.RegisterKnownEventTypes(typeof(BookingEvents.V1.BookingSubmitted).Assembly);

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .MinimumLevel.Override("Microsoft.AspNetCore.Mvc.Infrastructure", LogEventLevel.Warning)
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .WriteTo.Seq("http://localhost:5341")
    .CreateLogger();

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilog();

builder.Services
    .AddControllers()
    .AddJsonOptions(cfg => cfg.JsonSerializerOptions.ConfigureForNodaTime(DateTimeZoneProviders.Tzdb));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddEventuous(builder.Configuration);

builder.Services.AddSingleton<IAgentCalendarService, MockAgentCalendarService>();
builder.Services.AddSingleton<IPropertyCalendarService, MockPropertyCalendarService>();
builder.Services.AddSingleton<IDomainService, DomainService>();


var app = builder.Build();

app.UseSerilogRequestLogging();
app.AddEventuousLogs();
app.UseSwagger().UseSwaggerUI();
app.MapControllers();

app.UseSwagger();
//app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "PaymentSystems.WebAPI v1"));

var factory  = app.Services.GetRequiredService<ILoggerFactory>();
var listener = new LoggingEventListener(factory, "OpenTelemetry");

try {
    app.Run("http://*:5051");
    return 0;
}
catch (Exception e) {
    Log.Fatal(e, "Host terminated unexpectedly");
    return 1;
}
finally {
    Log.CloseAndFlush();
    listener.Dispose();
}