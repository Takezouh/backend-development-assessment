
using backend_development_assessment.api.DTOs;
using FluentValidation;

namespace backend_development_assessment.api.Validators;

public class CreateTicketRequestValidator: AbstractValidator<CreateTicketDTO>
{
    public CreateTicketRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.")
            .Length(3, 150).WithMessage("Name must be between 3 and 100 characters.");
        
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.").MaximumLength(150)
            .EmailAddress().WithMessage("Invalid email format.");

        RuleFor(x => x.Department)
            .NotEmpty().WithMessage("Department is required.")
            .Length(3, 100).WithMessage("Department must be between 3 and 100 characters.");

        RuleFor(x => x.Subject)
            .NotEmpty().WithMessage("Subject is required.")
            .Length(3, 200).WithMessage("Subject must be between 3 and 100 characters.");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Description is required.")
            .Length(3, 2000).WithMessage("Description must be between 3 and 100 characters.");

        RuleFor(x => x.Priority)
            .NotEmpty().WithMessage("Priority is required.")
            .IsInEnum();

        RuleFor(x => x.Status)
            .NotEmpty().WithMessage("Status is required.")
            .IsInEnum();
    }
}

public class UpdateTicketRequestValidator: AbstractValidator<UpdateTicketDTO>
{
    public UpdateTicketRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.")
            .Length(3, 150).WithMessage("Name must be between 3 and 100 characters.")
            .When(x => x.Name is not null);;
        
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.").MaximumLength(150)
            .EmailAddress().WithMessage("Invalid email format.")
            .When(x => x.Name is not null);;

        RuleFor(x => x.Department)
            .NotEmpty().WithMessage("Department is required.")
            .Length(3, 100).WithMessage("Department must be between 3 and 100 characters.")
            .When(x => x.Name is not null);;

        RuleFor(x => x.Subject)
            .NotEmpty().WithMessage("Subject is required.")
            .Length(3, 200).WithMessage("Subject must be between 3 and 100 characters.")
            .When(x => x.Name is not null);;

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Description is required.")
            .Length(3, 2000).WithMessage("Description must be between 3 and 100 characters.")
            .When(x => x.Name is not null);

        RuleFor(x => x.Priority)
            .NotEmpty().WithMessage("Priority is required.")
            .IsInEnum()
            .When(x => x.Name is not null);

        RuleFor(x => x.Status)
            .NotEmpty().WithMessage("Status is required.")
            .IsInEnum()
            .When(x => x.Name is not null);
    }
}