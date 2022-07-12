using MediatR;

namespace Wasm.Shared.Features.ManageTrails.EditTrail;

public sealed record GetTrailRequest(int TrailId) : IRequest<GetTrailRequest.Response>
{
    public const string RouteTemplate = "/api/trails/{trailId}";
    
    public sealed record RouteInstruction(int Id, int Stage, string Description);

    public sealed record Trail(int Id, string Name, string Location, string? Image, int TimeInMinutes, int Length, string Description,
                               IEnumerable<RouteInstruction> RouteInstructions);
    public sealed record Response(Trail? Trail);
}