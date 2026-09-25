using eScout_v2.Application.UseCases.Inputs;
using FluentValidation;

namespace eScout_v2.Validation;

public class ActivityInputValidator : AbstractValidator<ActivityInput>
{
    public ActivityInputValidator()
    {
        RuleLevelCascadeMode = CascadeMode.Stop;

        RuleFor(x => x.Title)
            .NotEmpty()
            .MaximumLength(64);

        RuleFor(x => x.Description)
            .NotEmpty()
            .MaximumLength(10000);

        RuleFor(x => x.EndTime)
            .GreaterThan(x => x.StartTime)
            .WithMessage("Activity end time must be greater than start time.");
    }
}