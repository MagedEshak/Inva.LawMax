using FluentValidation;
using Inva.LawCases.DTOs.Case;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inva.LawCases.Validations
{
    public class CaseEntityValidation : AbstractValidator<CreateUpdateCaseDto>
    {
        public CaseEntityValidation()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title is required.")
                .MaximumLength(200).WithMessage("Title must be at most 200 characters.")
                .When(d => d.Title != null);

            RuleFor(x => x.Description)
                .MaximumLength(1000).WithMessage("Description must be at most 1000 characters.")
            .When(d => d.Description != null);

            RuleFor(x => x.Status)
                .IsInEnum().WithMessage("Invalid status value.")
                .When(d => d.Status != null);

        }
    }
}
