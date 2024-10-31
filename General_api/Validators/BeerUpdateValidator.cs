using FluentValidation;
using General_api.DTOs;

namespace General_api.Validators
{
    public class BeerUpdateValidator : AbstractValidator<BeerUpdateDto>
    {
        public BeerUpdateValidator()
        {
            RuleFor(x => x.Id).NotNull().WithMessage(x => "The {PropertyName} is required!");
            RuleFor(x => x.Name).NotEmpty().WithMessage(x => "The {PropertyName} is required!");
            RuleFor(x => x.Name).Length(2, 20).WithMessage(x => "The {PropertyName} must be between 2 and 20 characters!");
            RuleFor(x => x.Alcohol).GreaterThanOrEqualTo(0);
        }
    }
}
