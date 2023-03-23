using Eddyproject.Common.Dtos.Address;
using FluentValidation;

namespace Eddyproject.Business.Validation;

public class AddressCreateValidator : AbstractValidator<AddressCreate>
{
    public AddressCreateValidator()
    {
        RuleFor(addressCreate => addressCreate.Email).NotEmpty().EmailAddress().MaximumLength(120);
        RuleFor(addressCreate => addressCreate.Country).NotEmpty().MaximumLength(120);
        RuleFor(addressCreate => addressCreate.City).NotEmpty().MaximumLength(120);
        RuleFor(addressCreate => addressCreate.Phone).MaximumLength(40);

    }
}
