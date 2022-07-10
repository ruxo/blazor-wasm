using FluentValidation;
using MediatR;

namespace Wasm.Shared.Features.ManageTrails;

public record AddTrailRequest(TrailViewModel Trail) : IRequest<AddTrailRequest.Response>
{
  public const string RouteTemplate = "/api/trails";
  public record Response(int? TrailId);
}

public sealed class AddTrailRequestValidator : AbstractValidator<AddTrailRequest>
{
    public AddTrailRequestValidator()
  {
    RuleFor(x => x.Trail).SetValidator(new TrailValidator());
  }
}