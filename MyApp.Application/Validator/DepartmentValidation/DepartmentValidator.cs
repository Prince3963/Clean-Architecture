using FluentValidation;
using MyApp.Application.DTOs;

namespace MyApp.Application.Validator.DepartmentValidation
{
    public class DepartmentValidator : AbstractValidator<AddDepartmentDTO>
    {
        public DepartmentValidator()
        {
            RuleFor(d => d.Name)
                .NotEmpty()
                .MaximumLength(100);
            RuleFor(d => d.Description)
                .MaximumLength(500);
        }

    }
}
