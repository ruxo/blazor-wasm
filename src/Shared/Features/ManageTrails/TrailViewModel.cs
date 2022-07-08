using FluentValidation;

namespace Wasm.Shared.Features.ManageTrails;

public sealed class TrailViewModel
{
    public int Id { get; set; }

    public string Name { get; set; } = "";
    public string Description { get; set; } = "";
    public string Location { get; set; } = "";
    public int TimeInMinutes { get; set; }
    public int Length { get; set; }
    public List<RouteInstruction> Route { get; set; } = new();
    
    public sealed class RouteInstruction
    {
        public int Stage { get; set; }

        public string Description { get; set; } = "";
    }
}

public sealed class TrailValidator : AbstractValidator<TrailViewModel>
{
    public TrailValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Plesae enter a name");
        RuleFor(x => x.Description).NotEmpty().WithMessage("Please enter a description");
        RuleFor(x => x.Location).NotEmpty().WithMessage("Please enter a location");
        RuleFor(x => x.Length).NotEmpty().WithMessage("Please enter a length");
        RuleFor(x => x.Route).NotEmpty().WithMessage("Please enter a route instruction");
        RuleForEach(x => x.Route).SetValidator(new RouteInstructionValidator());
    }
}

public sealed class RouteInstructionValidator : AbstractValidator<TrailViewModel.RouteInstruction>
{
    public RouteInstructionValidator()
    {
        RuleFor(x => x.Stage).NotEmpty().WithMessage("Please enter a stage");
        RuleFor(x => x.Description).NotEmpty().WithMessage("Please enter a description");
    }
}
