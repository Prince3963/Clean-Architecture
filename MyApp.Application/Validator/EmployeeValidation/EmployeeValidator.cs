using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using MyApp.Application.DTOs;
using MyApp.Domain.Entities;

namespace MyApp.Domain.Validator.EmployeeValidation
{
    public class AddEmployeeValidator : AbstractValidator<AddEmployeeDTO>
    {
        public AddEmployeeValidator()
        {
            RuleFor(e => e.FirstName)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(e => e.Email)
                .NotEmpty()
                .EmailAddress();

            RuleFor(e => e.Password)
                .NotEmpty()
                .MinimumLength(8)
                .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&]).+$")
                .WithMessage("Password must be at least 8 characters and contain uppercase, lowercase, number and special character.");

            RuleFor(e => e.Salary)
                .GreaterThan(0);
        }
    }
}