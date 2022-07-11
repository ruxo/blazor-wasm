using FluentValidation;
using MediatR;
using Wasm.Shared.Features.ManageTrails.Shared;

namespace Wasm.Shared.Features.ManageTrails.EditTrail;

public sealed record EditTrailRequest(TrailViewModel Trail) : IRequest<EditTrailRequest.Response>
{
    public const string RouteTemplate = "/api/trails";
    public sealed record Response(bool IsSuccess);
}

// ReSharper disable once UnusedType.Global
public sealed class EditTrailRequestValidator : AbstractValidator<EditTrailRequest>
{
    public EditTrailRequestValidator() {
        RuleFor(x => x.Trail).SetValidator(new TrailValidator());
    }
}