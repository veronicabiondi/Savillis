namespace Savillis.WebAPI.Application.Queries;

using Eventuous.Projections.MongoDB.Tools;
using NodaTime;

public record BookingDocument : ProjectedDocument {
    public BookingDocument(string id) : base(id) { }

    public string    PropertyId      { get; init; }
    public string    AgentId       { get; init; }
    public LocalDate Date  { get; init; }
}