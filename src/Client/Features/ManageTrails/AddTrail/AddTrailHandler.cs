using MediatR;
using System.Net.Http.Json;
using Wasm.Shared.Features.ManageTrails.AddTrail;

namespace Wasm.Client.Features.ManageTrails.AddTrail;

// ReSharper disable once UnusedType.Global
public sealed class AddTrailHandler : IRequestHandler<AddTrailRequest, AddTrailRequest.Response>
{
    readonly HttpClient http;

    public AddTrailHandler(HttpClient http)
    {
        this.http = http;
    }

    public async Task<AddTrailRequest.Response> Handle(AddTrailRequest request, CancellationToken cancellationToken)
    {
        var response = await http.PostAsJsonAsync(AddTrailRequest.RouteTemplate, request, cancellationToken);
        if (response.IsSuccessStatusCode)
        {
            var trailId = await response.Content.ReadFromJsonAsync<int>(cancellationToken: cancellationToken);
            return new(trailId);
        }
        else
            return new(null);
    }
}