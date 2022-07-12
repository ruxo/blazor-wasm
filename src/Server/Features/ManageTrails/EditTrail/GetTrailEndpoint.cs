using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Wasm.Server.Persistence;
using Wasm.Server.Persistence.Entities;
using Wasm.Shared.Features.ManageTrails.EditTrail;

namespace Wasm.Server.Features.ManageTrails.EditTrail;

public sealed class GetTrailEndpoint : EndpointBaseAsync.WithRequest<int>.WithActionResult<GetTrailRequest.Response>
{
    readonly BlazingTrailsContext db;
    public GetTrailEndpoint(BlazingTrailsContext db) {
        this.db = db;
    }

    [HttpGet(GetTrailRequest.RouteTemplate)]
    public override async Task<ActionResult<GetTrailRequest.Response>> HandleAsync(int trailId, CancellationToken cancellationToken = new ()) {
        var trail = await db.Trails.Include(x => x.Route).SingleOrDefaultAsync(x => x.Id == trailId, cancellationToken);
        return trail is null
                   ? BadRequest("Trail could not be found.")
                   : Ok(new GetTrailRequest.Response(ToTrailResponse(trail)));
    }

    GetTrailRequest.Trail ToTrailResponse(Trail trail) =>
        new(trail.Id,
            trail.Name,
            trail.Location,
            trail.Image,
            trail.TimeInMinutes,
            trail.Length,
            trail.Description,
            trail.Route.Select(ri => new GetTrailRequest.RouteInstruction(ri.Id, ri.Stage, ri.Description)));
}