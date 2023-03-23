using Eddyproject.Common.Dtos.Address;
using Eddyproject.Common.Dtos.Budget;
using Eddyproject.Common.Dtos.Student;
using FluentValidation;

namespace Eddyproject.Business.Validation;

public class BudgetCreateValidator : AbstractValidator<BudgetCreate>
{
    public BudgetCreateValidator()
    {
        RuleFor(budgetCreate => budgetCreate.Currency).NotEmpty().MaximumLength(50);
        RuleFor(budgetCreate => budgetCreate.Amount).NotEmpty().MaximumLength(50);

    }
}
