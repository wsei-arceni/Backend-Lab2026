using AppCore.Dto;
using AppCore.Interfaces;
using AppCore.Validators.Shared;
using FluentValidation;

namespace AppCore.Validators;

public class CreatePersonDtoValidator : AbstractValidator<CreatePersonDto>
{
    private readonly ICompanyRepository _companyRepository;
    
    public CreatePersonDtoValidator(ICompanyRepository companyRepository)
    {
        _companyRepository = companyRepository;
        
        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("Name cannot be empty")
            .MaximumLength(100).WithMessage("Name have to be less than 100 characters")
            .Matches(@"^[\p{L}\s\-]+$").WithMessage("Name can't contain special characters");
        
        RuleFor(x => x.LastName)
	        .MaximumLength(200).WithMessage("Last name have to be less than 200 symbols")
	        .Matches(@"^[\p{L}\s\-]+$").WithMessage("Last name can't contain special characters");
        
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email can't be empty")
            .EmailAddress().WithMessage("Incorrect email format")
            .MaximumLength(200);

        RuleFor(x => x.Phone)
	        .Matches(@"^\+(\d){10,14}$").WithMessage("Incorrect phone format")
	        .When(x => x.Phone is not null);

        RuleFor(x => x.BirthDate)
            .LessThan(DateTime.Today.AddYears(-18)).WithMessage("Person have to be over 18 years old")
            .GreaterThan(DateTime.Today.AddYears(-120)).WithMessage("Incorrect birth date")
            .When(x => x.BirthDate.HasValue);

        RuleFor(x => x.Gender)
            .IsInEnum().WithMessage("Incorrect Gender value");

        RuleFor(x => x.EmployerId)
            .MustAsync(EmployerExistsAsync).WithMessage("Provided organisation does not exist")
            .When(x => x.EmployerId.HasValue);

        RuleFor(x => x.Address)
            .SetValidator(new AddressDtoValidator()!)
            .When(x => x.Address is not null);
    }

    private async Task<bool> EmployerExistsAsync(
        Guid? employerId,
        CancellationToken ct) =>
        await _companyRepository.FindByIdAsync(employerId ?? Guid.Empty) is not null;
}