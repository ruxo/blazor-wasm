using System.Net.Http.Json;
using MediatR;
using Wasm.Shared.Features.ManageTrails.EditTrail;

namespace Wasm.Client.Features.ManageTrails.EditTrail;

// ReSharper disable once UnusedType.Global
public sealed class EditTrailHandler : IRequestHandler<EditTrailRequest, EditTrailRequest.Response>
{
    readonly HttpClient http;
    public EditTrailHandler(HttpClient http) {
        this.http = http;
    }

    public async Task<EditTrailRequest.Response> Handle(EditTrailRequest request, CancellationToken cancellationToken) {
        using var response = await http.PutAsJsonAsync(EditTrailRequest.RouteTemplate, request, cancellationToken);
        return new(response.IsSuccessStatusCode);
    }
}