using FluentValidation;
using Application.DTOs;
using System.Data;

namespace ApiStreetFighter.Validators
{
    public class CreateUpdateEntityDTOValidator : AbstractValidator<CreateUpdateEntityDTO>
    {
        public CreateUpdateEntityDTOValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MinimumLength(7)
                .WithMessage("The name of the fighter is mandatory");
            RuleFor(x => x.Attack)
                .GreaterThan(1)
                .WithMessage("Attack cannot be lesser than 1");
        }
    }
}
