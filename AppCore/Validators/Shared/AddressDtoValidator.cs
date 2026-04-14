using AppCore.Dto;
using FluentValidation;

namespace AppCore.Validators.Shared;

public class AddressDtoValidator : AbstractValidator<AddressDto>
{
    public AddressDtoValidator()
    {
        RuleFor(x => x.Street)
            .NotEmpty()
            .MaximumLength(200).WithMessage("Street name must not exceed 200 characters");
        RuleFor(x => x.City)
            .NotEmpty()
            .MaximumLength(200).WithMessage("City name must not exceed 200 characters");
        RuleFor(x => x.PostalCode)
            .NotEmpty()
            .Matches(@"^\d{2}\-\d{3}$").WithMessage("Incorrect postal code format");
        RuleFor(x => x.Country)
            .NotEmpty()
            .MaximumLength(100).WithMessage("Country name must not exceed 100 characters");
        RuleFor(x => x.Type)
            .NotEmpty()
            .IsInEnum().WithMessage("Incorrect type");
    }
}