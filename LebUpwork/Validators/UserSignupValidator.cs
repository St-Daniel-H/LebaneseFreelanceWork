using FluentValidation;
using LebUpwork.Api.Resources;

namespace LebUpwork.Api.Validators
{
    public class UserSignupValidator: AbstractValidator<UserSignupResources>
    {
        public UserSignupValidator()
        {


            RuleFor(a => a.Email)
                .NotEmpty()
                .WithMessage("Email must not be empty")
                .MaximumLength(255)
                .WithMessage("Email must not exceed 255 character")
                .EmailAddress().WithMessage("Invalid email address format.");

            RuleFor(a => a.FirstName)
                .NotEmpty()
                .WithMessage("First name must not be empty")
                .MaximumLength(255)
                .WithMessage("First name must not exceed 255 character");

            RuleFor(a => a.LastName)
                .NotEmpty()
                .WithMessage("Last Name must not be empty")
                .MaximumLength(255)
                .WithMessage("Last Name must not exceed 255 character");
            RuleFor(a => a.Password)
                .NotEmpty().WithMessage("Password must not be empty")
                .MinimumLength(8).WithMessage("Password must be at least 8 characters long")
                .MaximumLength(255).WithMessage("Password must not exceed 255 characters")
                .Matches("[A-Z]").WithMessage("Password must contain at least one uppercase letter")
                .Matches("[a-z]").WithMessage("Password must contain at least one lowercase letter")
                .Matches("[0-9]").WithMessage("Password must contain at least one numeric digit")
                .Matches("[^a-zA-Z0-9]").WithMessage("Password must contain at least one special character");

        }
    }
}
