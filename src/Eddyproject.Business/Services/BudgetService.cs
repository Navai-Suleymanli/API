using AutoMapper;
using Eddyproject.Common.Dtos.Budget;
using Eddyproject.Common.Interfaces;
using Eddyproject.Common.Model;

namespace Eddyproject.Business.Services;

public class BudgetService : IBudgetService
{
    private IMapper Mapper { get; }
    private IGenericRepository<Budget> BudgetRepository { get; }

    public BudgetService(IMapper mapper, IGenericRepository<Budget> BudgetRepository)
    {
        Mapper = mapper;
        this.BudgetRepository = BudgetRepository;
    }


    public async Task<int> CreateBudgetAsync(BudgetCreate budgetCreate)
    {
        var entity = Mapper.Map<Budget>(budgetCreate);
        await BudgetRepository.InsertAsync(entity);
        await BudgetRepository.SaveChangesAsync();
        return entity.Id;
    }

    public async Task DeleteBudgetAsync(BudgetDelete budgetDelete)
    {
        var entity = await BudgetRepository.GetByIdAsync(budgetDelete.Id);
        BudgetRepository.Delete(entity);
        await BudgetRepository.SaveChangesAsync();
    }

    public async Task<BudgetGet> GetBudgetAsync(int id)
    {
        var entity = await BudgetRepository.GetByIdAsync(id);
        return Mapper.Map<BudgetGet>(entity);
    }

    public async Task<List<BudgetGet>> GetBudgetsAsync()
    {
        var entities = await BudgetRepository.GetAsync(null, null);
        return Mapper.Map<List<BudgetGet>>(entities);
    }

    public async Task UpdateBudgetAsync(BudgetUpdate budgetUpdate)
    {
        var entity = Mapper.Map<Budget>(budgetUpdate);
        BudgetRepository.Update(entity);
        await BudgetRepository.SaveChangesAsync();
    }
}
