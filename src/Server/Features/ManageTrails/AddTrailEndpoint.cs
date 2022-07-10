using Ardalis.ApiEndpoints;

namespace Wasm.Server.Features.ManageTrails;

public sealed class AddTrailEndpoint : EndpointBaseAsync.WithRequest<AddTrailEndpoint>.WithResult<int>
{
    public override Task<int> HandleAsync(AddTrailEndpoint request, CancellationToken cancellationToken = new CancellationToken()) {
        throw new NotImplementedException();
    }
}