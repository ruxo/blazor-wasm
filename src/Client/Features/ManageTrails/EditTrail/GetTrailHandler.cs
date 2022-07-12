using System.Net.Http.Json;
using MediatR;
using RZ.Foundation.Extensions;
using Wasm.Shared.Features.ManageTrails.EditTrail;
using static LanguageExt.Prelude;

namespace Wasm.Client.Features.ManageTrails.EditTrail;

// ReSharper disable once UnusedType.Global
public sealed class GetTrailHandler : IRequestHandler<GetTrailRequest, GetTrailRequest.Response>
{
    readonly HttpClient http;
    public GetTrailHandler(HttpClient http) {
        this.http = http;
    }

    public async Task<GetTrailRequest.Response> Handle(GetTrailRequest request, CancellationToken cancellationToken) {
        var uri = GetTrailRequest.RouteTemplate.Replace("{trailId}", request.TrailId.ToString());
        var trail = await OptionalAsync(_ => http.GetFromJsonAsync<GetTrailRequest.Trail>(uri, cancellationToken: cancellationToken)).ToOption();
        return new(trail.GetOrDefault());
    }
}