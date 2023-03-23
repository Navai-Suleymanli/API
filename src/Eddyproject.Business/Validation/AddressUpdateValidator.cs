using Eddyproject.Common.Dtos.Address;
using FluentValidation;

namespace Eddyproject.Business.Validation;

public class AddressUpdateValidator : AbstractValidator<AddressCreate>
{
    public AddressUpdateValidator()
    {
        RuleFor(addressUpdate => addressUpdate.Email).NotEmpty().EmailAddress().MaximumLength(120);
        RuleFor(addressUpdate => addressUpdate.Country).NotEmpty().MaximumLength(120);
        RuleFor(addressUpdate => addressUpdate.City).NotEmpty().MaximumLength(120);
        RuleFor(addressUpdate => addressUpdate.Phone).MaximumLength(40);

    }
}
