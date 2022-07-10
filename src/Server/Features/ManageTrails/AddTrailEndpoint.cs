using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using Wasm.Server.Persistence;
using Wasm.Server.Persistence.Entities;
using Wasm.Shared.Features.ManageTrails;

namespace Wasm.Server.Features.ManageTrails;

public sealed class AddTrailEndpoint : EndpointBaseAsync.WithRequest<AddTrailRequest>.WithResult<int>
{
    readonly BlazingTrailsContext db;

    public AddTrailEndpoint(BlazingTrailsContext db) {
        this.db = db;
    }

    [HttpPost(AddTrailRequest.RouteTemplate)]
    public override async Task<int> HandleAsync(AddTrailRequest request, CancellationToken cancellationToken = new()) {
        var trail = new Trail{
            Name = request.Trail.Name,
            Description = request.Trail.Description,
            Location = request.Trail.Location,
            TimeInMinutes = request.Trail.TimeInMinutes,
            Length = request.Trail.Length,
            Route = new List<RouteInstruction>()
        };
        await db.Trails.AddAsync(trail, cancellationToken);
        var routeInstructions =
            request.Trail.Route
                   .Select(x => new RouteInstruction{
                        Stage = x.Stage,
                        Description = x.Description,
                        Trail = trail
                    }).ToArray();

        await db.RouteInstructions.AddRangeAsync(routeInstructions, cancellationToken);
        await db.SaveChangesAsync(cancellationToken);
        return trail.Id;
    }
}