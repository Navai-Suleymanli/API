using Eddyproject.Common.Dtos.Course;
using FluentValidation;

namespace Eddyproject.Business.Validation;

public class CourseUpdateValidator : AbstractValidator<CourseUpdate>
{
    public CourseUpdateValidator()
    {
        RuleFor(courseUpdate => courseUpdate.Name).NotEmpty().MaximumLength(50);

    }
}
