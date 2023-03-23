using Eddyproject.Common.Dtos.Address;
using Eddyproject.Common.Dtos.Student;
using FluentValidation;

namespace Eddyproject.Business.Validation;

public class StudentUpdateValidator : AbstractValidator<StudentCreate>
{
    public StudentUpdateValidator()
    {
        RuleFor(studentUpdate => studentUpdate.FirstName).NotEmpty().MaximumLength(50);
        RuleFor(studentUpdate => studentUpdate.LastName).NotEmpty().MaximumLength(50);

    }

    internal Task ValidateAsync(StudentUpdate studentUpdate)
    {
        throw new NotImplementedException();
    }
}
