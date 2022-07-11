using MediatR;
using Wasm.Shared.Features.ManageTrails.Shared;

namespace Wasm.Client.Features.ManageTrails;

// ReSharper disable once UnusedType.Global
public sealed class UploadTrailImageHandler : IRequestHandler<UploadTrailImageRequest, UploadTrailImageRequest.Response>
{
    readonly HttpClient http;
    public UploadTrailImageHandler(HttpClient http) {
        this.http = http;
    }

    public async Task<UploadTrailImageRequest.Response> Handle(UploadTrailImageRequest request, CancellationToken cancellationToken) {
        var fileContent = request.File.OpenReadStream(request.File.Size, cancellationToken);

        using var content = new MultipartFormDataContent();
        content.Add(new StreamContent(fileContent), "image", request.File.Name);

        var targetUri = UploadTrailImageRequest.RouteTemplate.Replace("{trailId}", request.TrailId.ToString());
        var response = await http.PostAsync(targetUri, content, cancellationToken);

        if (response.IsSuccessStatusCode) {
            var fileName = await response.Content.ReadAsStringAsync(cancellationToken: cancellationToken);
            return new(fileName);
        }
        else
            return new(null);
    }
}