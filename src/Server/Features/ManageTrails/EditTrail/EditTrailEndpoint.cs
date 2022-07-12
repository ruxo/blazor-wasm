using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Wasm.Server.Persistence;
using Wasm.Server.Persistence.Entities;
using Wasm.Shared.Features.ManageTrails.EditTrail;

namespace Wasm.Server.Features.ManageTrails.EditTrail;

public sealed class EditTrailEndpoint : EndpointBaseAsync.WithRequest<EditTrailRequest>.WithActionResult<bool>
{
    readonly BlazingTrailsContext db;
    public EditTrailEndpoint(BlazingTrailsContext db) {
        this.db = db;
    }

    [HttpPut(EditTrailRequest.RouteTemplate)]
    public override async Task<ActionResult<bool>> HandleAsync(EditTrailRequest request, CancellationToken cancellationToken = new ()) {
        var trail = await db.Trails.Include(x => x.Route).SingleOrDefaultAsync(x => x.Id == request.Trail.Id, cancellationToken);
        if (trail is null) return BadRequest("Trail could not be found.");

        trail.Name = request.Trail.Name;
        trail.Description = request.Trail.Description;
        trail.Location = request.Trail.Location;
        trail.TimeInMinutes = request.Trail.TimeInMinutes;
        trail.Length = request.Trail.Length;
        trail.Route = request.Trail.Route.Select(ri => new RouteInstruction{ Stage = ri.Stage, Description = ri.Description, Trail = trail }).ToList();
        if (trail.Image != request.Trail.Image) {
            if (trail.Image != null)
                System.IO.File.Delete(Path.Combine(Directory.GetCurrentDirectory(), "Images", trail.Image));
            trail.Image = request.Trail.Image;
        }
        await db.SaveChangesAsync(cancellationToken);
        return Ok(true);
    }
}