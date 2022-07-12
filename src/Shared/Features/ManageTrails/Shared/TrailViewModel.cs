using FluentValidation;

namespace Wasm.Shared.Features.ManageTrails.Shared;

public sealed class TrailViewModel
{
    public int Id { get; set; }

    public string Name { get; set; } = "";
    public string Description { get; set; } = "";
    public string Location { get; set; } = "";
    public int TimeInMinutes { get; set; }
    public int Length { get; set; }
    public List<RouteInstruction> Route { get; set; } = new();

    public string? Image { get; set; }

    public sealed class RouteInstruction
    {
        public int Stage { get; set; }

        public string Description { get; set; } = "";

        public RouteInstruction Clone() => (RouteInstruction)MemberwiseClone();
    }

    public TrailViewModel Clone() {
        var instance = (TrailViewModel) MemberwiseClone();
        instance.Route = Route.Select(ri => ri.Clone()).ToList();
        return instance;
    }
}

public sealed class TrailValidator : AbstractValidator<TrailViewModel>
{
    public TrailValidator() {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Please enter a name");
        RuleFor(x => x.Description).NotEmpty().WithMessage("Please enter a description");
        RuleFor(x => x.Location).NotEmpty().WithMessage("Please enter a location");
        RuleFor(x => x.Length).GreaterThan(0);
        RuleFor(x => x.Route).NotEmpty().WithMessage("Please enter a route instruction");
        RuleFor(x => x.TimeInMinutes).GreaterThan(0).WithName("Time");
        RuleForEach(x => x.Route).SetValidator(new RouteInstructionValidator());
    }
}

public sealed class RouteInstructionValidator : AbstractValidator<TrailViewModel.RouteInstruction>
{
    public RouteInstructionValidator() {
        RuleFor(x => x.Stage).GreaterThan(0);
        RuleFor(x => x.Description).NotEmpty().WithMessage("Please enter a description");
    }
}