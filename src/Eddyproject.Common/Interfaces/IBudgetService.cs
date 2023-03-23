using Eddyproject.Common.Dtos.Budget;

namespace Eddyproject.Common.Interfaces;

public interface IBudgetService
{
    Task<int> CreateBudgetAsync(BudgetCreate budgetCreate);
    Task UpdateBudgetAsync(BudgetUpdate budgetUpdate);
    Task<List<BudgetGet>> GetBudgetsAsync();

    Task<BudgetGet> GetBudgetAsync(int id);
    Task DeleteBudgetAsync(BudgetDelete budgetDelete);
}
