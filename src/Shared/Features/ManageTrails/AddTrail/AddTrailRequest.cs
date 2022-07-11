using FluentValidation;
using MediatR;
using Wasm.Shared.Features.ManageTrails.Shared;

namespace Wasm.Shared.Features.ManageTrails.AddTrail;

public record AddTrailRequest(TrailViewModel Trail) : IRequest<AddTrailRequest.Response>
{
    public const string RouteTemplate = "/api/trails";
    public record Response(int? TrailId);
}

// ReSharper disable once UnusedType.Global
public sealed class AddTrailRequestValidator : AbstractValidator<AddTrailRequest>
{
    public AddTrailRequestValidator()
    {
        RuleFor(x => x.Trail).SetValidator(new TrailValidator());
    }
}