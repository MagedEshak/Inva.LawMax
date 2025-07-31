using FluentValidation;
using Inva.LawCases.DTOs.Hearing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inva.LawCases.Validations
{
    public class HearingEntityValidation : AbstractValidator<CreateUpdateHearingDto>
    {
        public HearingEntityValidation()
        {
            RuleFor(x => x.Date)
            .GreaterThan(DateTime.MinValue).WithMessage("Date must be valid.")
            .LessThan(DateTime.Now.AddYears(5)).WithMessage("Date must be within 5 years.")
            .When(d => d.Date != null);


            RuleFor(x => x.Location)
                .NotEmpty().WithMessage("Location is required.")
                .MaximumLength(200).WithMessage("Location must be at most 200 characters.")
                .When(d => d.Location != null);

            RuleFor(x => x.Decision)
                .NotEmpty().WithMessage("Location is required.")
                .MaximumLength(200).WithMessage("Location must be at most 200 characters.")
                .When(d => d.Decision != null);


        }
    }
}
