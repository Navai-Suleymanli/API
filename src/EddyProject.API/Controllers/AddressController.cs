using Eddyproject.Common.Dtos.Address;
using Eddyproject.Common.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
namespace EddyProject.API.Controllers;

[ApiController]
[Authorize]
[Route("api/address")]

public class AddressController : ControllerBase
{
    private IAddressService AddressService { get; }
    public AddressController(IAddressService addressService)
    {
        AddressService = addressService;
    }

    [HttpPost("Create")]
  //  [Route("Create")]
    public async Task<IActionResult> CreateAddress(AddressCreate addressCreate)
    {
        var id = await AddressService.CreateAddressAsync(addressCreate);
        return Ok(id);
    }

    [HttpPut("Update")]
 //   [Route("Update")]
    public async Task<IActionResult> UpdateAddress(AddressUpdate addressUpdate)
    {
        await AddressService.UpdateAddressAsync(addressUpdate);
        return Ok();
    }
    [HttpDelete("Delete")]
  //  [Route("Delete")]
    public async Task<IActionResult> DeleteAddress(AddressDelete addressDelete)
    {
        await AddressService.DeleteAddressAsync(addressDelete);
        return Ok();
    }
    [EnableCors("Policy1")]
    [HttpGet("GetById")]
    public async Task<IActionResult> GetAddress(int id)
    {
        var address = await AddressService.GetAddressAsync(id);
        return Ok(address);
    }

    [EnableCors("AnotherPolicy")]
    [HttpGet("Get")]
  //  [Route("Get")]
    public async Task<IActionResult> GetAddresses()
    {
        var whitelist = new List<string>() { "khatai.suleymanli05@gmail.com" };
        var email = HttpContext.User.Claims.First(c => c.Type == "Preffered_username").Value;

        if (!whitelist.Contains(email))
            return new ForbidResult();

        var addresses = await AddressService.GetAddressesAsync();
        return Ok(addresses);
    }




}
