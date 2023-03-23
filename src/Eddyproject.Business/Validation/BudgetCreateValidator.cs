using Eddyproject.Common.Dtos.Address;
using Eddyproject.Common.Dtos.Budget;
using Eddyproject.Common.Dtos.Student;
using FluentValidation;

namespace Eddyproject.Business.Validation;

public class BudgetUpdateValidator : AbstractValidator<BudgetCreate>
{
    public BudgetUpdateValidator()
    {
        RuleFor(budgetUpdate => budgetUpdate.Currency).NotEmpty().MaximumLength(50);
        RuleFor(budgetUpdate => budgetUpdate.Amount).NotEmpty().MaximumLength(50);

    }
}
