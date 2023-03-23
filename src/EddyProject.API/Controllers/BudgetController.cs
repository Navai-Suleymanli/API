using Eddyproject.Common.Dtos.Budget;
using Eddyproject.Common.Interfaces;
using Microsoft.AspNetCore.Mvc;
namespace EddyProject.API.Controllers;

[ApiController]
[Route("api/budget")]

public class BudgetController : ControllerBase
{
    private IBudgetService BudgetService { get; }
    public BudgetController(IBudgetService budgetService)
    {
        BudgetService = budgetService;
    }

    [HttpPost]
    [Route("Create")]
    public async Task<IActionResult> CreateBudget(BudgetCreate budgetCreate)
    {
        var id = await BudgetService.CreateBudgetAsync(budgetCreate);
        return Ok(id);
    }

    [HttpPut]
    [Route("Update")]
    public async Task<IActionResult> UpdateBudget(BudgetUpdate budgetUpdate)
    {
        await BudgetService.UpdateBudgetAsync(budgetUpdate);
        return Ok();
    }
    [HttpDelete]
    [Route("Delete")]
    public async Task<IActionResult> DeleteBudget(BudgetDelete budgetDelete)
    {
        await BudgetService.DeleteBudgetAsync(budgetDelete);
        return Ok();
    }

    [HttpGet]
    [Route("Get/{id}")]
    public async Task<IActionResult> GetBudget(int id)
    {
        var budget = await BudgetService.GetBudgetAsync(id);
        return Ok(budget);
    }

    [HttpGet]
    [Route("Get")]
    public async Task<IActionResult> GetBudgets()
    {
        var budgets = await BudgetService.GetBudgetsAsync();
        return Ok(budgets);
    }
}
