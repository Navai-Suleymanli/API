using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eddyproject.Common.Dtos.Address;

namespace Eddyproject.Common.Interfaces;

public interface IAddressService
{
    Task<int> CreateAddressAsync(AddressCreate addressCreate);
    Task UpdateAddressAsync(AddressUpdate addressUpdate);
    Task DeleteAddressAsync(AddressDelete addressDelete);
    Task<AddressGet> GetAddressAsync(int id);
    Task<List<AddressGet>> GetAddressesAsync();
}
