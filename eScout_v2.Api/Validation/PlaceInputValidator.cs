using eScout_v2.Application.UseCases.Inputs;
using FluentValidation;

namespace eScout_v2.Validation;

public class PlaceInputValidator : AbstractValidator<PlaceInput>
{
    public PlaceInputValidator()
    {
        RuleLevelCascadeMode = CascadeMode.Stop;

        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(32);

        RuleFor(x => x.Description)
            .NotEmpty()
            .MaximumLength(2048);
    }
}