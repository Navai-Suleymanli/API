using Eddyproject.Common.Dtos.Address;
using Eddyproject.Common.Dtos.Student;
using FluentValidation;

namespace Eddyproject.Business.Validation;

public class StudentCreateValidator : AbstractValidator<StudentCreate>
{
    public StudentCreateValidator()
    {
        RuleFor(studentCreate => studentCreate.FirstName).NotEmpty().MaximumLength(50);
        RuleFor(addressCreate => addressCreate.LastName).NotEmpty().MaximumLength(50);

    }
}
