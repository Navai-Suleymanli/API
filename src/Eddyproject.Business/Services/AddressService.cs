using AutoMapper;
using Eddyproject.Business.Exceptions;
using Eddyproject.Business.Validation;
using Eddyproject.Common.Dtos.Address;
using Eddyproject.Common.Interfaces;
using Eddyproject.Common.Model;
using FluentValidation;

namespace Eddyproject.Business.Services;

public class AddressService : IAddressService
{
    private IMapper Mapper { get; }
    private IGenericRepository<Address> AddressRepository { get; }
    private AddressCreateValidator AddressCreateValidator { get; }
    private AddressUpdateValidator AddressUpdateValidator { get; }

    public AddressService(IMapper mapper, IGenericRepository<Address> addressRepository,
        AddressCreateValidator addressCreateValidator, AddressUpdateValidator addressUpdateValidator)
    {
        Mapper = mapper;
        AddressRepository = addressRepository;
        AddressCreateValidator = addressCreateValidator;
        AddressUpdateValidator = addressUpdateValidator;
    }


    public async Task<int> CreateAddressAsync(AddressCreate addressCreate)
    {
        await AddressCreateValidator.ValidateAndThrowAsync(addressCreate);

        var entity = Mapper.Map<Address>(addressCreate);
        await AddressRepository.InsertAsync(entity);
        await AddressRepository.SaveChangesAsync();
        return entity.Id;
    }

    public async Task DeleteAddressAsync(AddressDelete addressDelete)
    {
        var entity = await AddressRepository.GetByIdAsync(addressDelete.Id, (address) => address.Students);
        if(entity == null)
            throw new AddressNotFoundException(addressDelete.Id);

        if (entity.Students.Count > 0)
            throw new DependentStudentsExistException(entity.Students);
        
        AddressRepository.Delete(entity);
        await AddressRepository.SaveChangesAsync();
    }

    public async Task<AddressGet> GetAddressAsync(int id)
    {
        var entity = await AddressRepository.GetByIdAsync(id);
        if(entity == null)
            throw new AddressNotFoundException(id);
        return Mapper.Map<AddressGet>(entity);
    }

    public async Task<List<AddressGet>> GetAddressesAsync()
    {
        var entities = await AddressRepository.GetAsync(null, null);
        return Mapper.Map<List<AddressGet>>(entities);
    }

    public async Task UpdateAddressAsync(AddressUpdate addressUpdate)
    {
       // await AddressUpdateValidator.ValidateAndThrowAsync(addressUpdate); //error verir burda

        var existingAddress = await AddressRepository.GetByIdAsync(addressUpdate.Id);

        if (existingAddress == null)
            throw new AddressNotFoundException(addressUpdate.Id);

        var entity = Mapper.Map<Address>(addressUpdate);
        AddressRepository.Update(entity);
        await AddressRepository.SaveChangesAsync();
    }
}
