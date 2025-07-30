using FluentValidation;
using Inva.LawMax.DTOs.Lawyer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inva.LawCases.Validations
{
    public class LawyerEntityValidation : AbstractValidator<CreateUpdateLawyerDto>
    {
        public LawyerEntityValidation()
        {
            RuleFor(x => x.Name)
               .NotEmpty().WithMessage("Name is required.")
               .MaximumLength(100).WithMessage("Name must be at most 100 characters.")
               .When(x => x.Name != null);

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email format.")
                .MaximumLength(100).WithMessage("Email must be at most 100 characters.")
                   .When(x => x.Email != null);

            RuleFor(x => x.Phone)
                .NotEmpty().WithMessage("Phone number is required.")
                .Matches(@"^\+?\d{10,15}$").WithMessage("Invalid phone number format.")
                .When(x => x.Phone != null);

            RuleFor(x => x.Address)
                .NotEmpty().WithMessage("Address is required.")
                .MaximumLength(200).WithMessage("Address must be at most 200 characters.")
                .When(x => x.Address != null);

            RuleFor(x => x.Speciality)
                .NotEmpty().WithMessage("Speciality is required.")
                .MaximumLength(100).WithMessage("Speciality must be at most 100 characters.")
                .When(x => x.Speciality != null);

        }
    }
}

