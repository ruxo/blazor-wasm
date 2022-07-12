using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using Wasm.Server.Persistence;
using Wasm.Shared.Features.ManageTrails.Shared;

namespace Wasm.Server.Features.ManageTrails.Shared;

public sealed class UploadTrailImageEndpoint : EndpointBaseAsync.WithRequest<int>.WithActionResult<string>
{
    readonly BlazingTrailsContext db;
    public UploadTrailImageEndpoint(BlazingTrailsContext db) {
        this.db = db;
    }

    [HttpPost(UploadTrailImageRequest.RouteTemplate)]
    public override async Task<ActionResult<string>> HandleAsync(int trailId, CancellationToken cancellationToken = default) {
        var trail = await db.Trails.SingleOrDefaultAsync(x => x.Id == trailId, cancellationToken);
        if (trail is null) return BadRequest("Trail does not exist.");

        var file = Request.Form.Files[0];
        if (file.Length == 0) return BadRequest("No image found.");

        var filename = $"{Guid.NewGuid()}.jpg";
        var saveLocation = Path.Combine(Directory.GetCurrentDirectory(), "Images", filename);

        var resizeOptions = new ResizeOptions{
            Mode = ResizeMode.Pad,
            Size = new(640, 426)
        };
        using var image = await Image.LoadAsync(file.OpenReadStream(), cancellationToken);
        image.Mutate(x => x.Resize(resizeOptions));
        await image.SaveAsJpegAsync(saveLocation, cancellationToken);
        
        if (trail.Image != null)
            System.IO.File.Delete(Path.Combine(Directory.GetCurrentDirectory(), "Images", trail.Image));

        trail.Image = filename;
        await db.SaveChangesAsync(cancellationToken);

        return Ok(trail.Image);
    }
}