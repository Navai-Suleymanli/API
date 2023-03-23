using AutoMapper;
using Eddyproject.Business.Exceptions;
using Eddyproject.Business.Validation;
using Eddyproject.Common.Dtos.Budget;
using Eddyproject.Common.Interfaces;
using Eddyproject.Common.Model;
using FluentValidation;

namespace Eddyproject.Business.Services;

public class BudgetService : IBudgetService
{
    private IMapper Mapper { get; }
    private IGenericRepository<Budget> BudgetRepository { get; }
    private BudgetCreateValidator BudgetCreateValidator { get; }
    private BudgetUpdateValidator BudgetUpdateValidator { get; }

    public BudgetService(IMapper mapper, IGenericRepository<Budget> BudgetRepository,
        BudgetCreateValidator budgetCreateValidator, BudgetUpdateValidator budgetUpdateValidator)
    {
        Mapper = mapper;
        this.BudgetRepository = BudgetRepository;
        BudgetCreateValidator = budgetCreateValidator;
        BudgetUpdateValidator = budgetUpdateValidator;
    }


    public async Task<int> CreateBudgetAsync(BudgetCreate budgetCreate)
    {
        await BudgetCreateValidator.ValidateAndThrowAsync(budgetCreate);

        var entity = Mapper.Map<Budget>(budgetCreate);
        await BudgetRepository.InsertAsync(entity);
        await BudgetRepository.SaveChangesAsync();
        return entity.Id;
    }

    public async Task DeleteBudgetAsync(BudgetDelete budgetDelete)
    {
        var entity = await BudgetRepository.GetByIdAsync(budgetDelete.Id, (budget) => budget.Students);

        if (entity == null)
            throw new BudgetNotFoundException(budgetDelete.Id);

        if (entity.Students.Count > 0)
            throw new DependentStudentsExistException(entity.Students);

        BudgetRepository.Delete(entity);
        await BudgetRepository.SaveChangesAsync();
    }

    public async Task<BudgetGet> GetBudgetAsync(int id)
    {
        var entity = await BudgetRepository.GetByIdAsync(id);

        if (entity == null)
            throw new BudgetNotFoundException(id);

        return Mapper.Map<BudgetGet>(entity);
    }

    public async Task<List<BudgetGet>> GetBudgetsAsync()
    {
        var entities = await BudgetRepository.GetAsync(null, null);
        return Mapper.Map<List<BudgetGet>>(entities);
    }

    public async Task UpdateBudgetAsync(BudgetUpdate budgetUpdate)
    {
        //await BudgetUpdateValidator.ValidateAndThrowAsync(budgetUpdate);

        var existingBudget = await BudgetRepository.GetByIdAsync(budgetUpdate.Id);

        if (existingBudget == null)
            throw new BudgetNotFoundException(budgetUpdate.Id);

        var entity = Mapper.Map<Budget>(budgetUpdate);
        BudgetRepository.Update(entity);
        await BudgetRepository.SaveChangesAsync();
       
    }
}


