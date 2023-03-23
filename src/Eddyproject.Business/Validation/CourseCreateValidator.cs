using Eddyproject.Common.Dtos.Course;
using FluentValidation;

namespace Eddyproject.Business.Validation;

public class CourseCreateValidator : AbstractValidator<CourseCreate>
{
    public CourseCreateValidator()
    {
        RuleFor(courseCreate => courseCreate.Name).NotEmpty().MaximumLength(50);

    }
}
