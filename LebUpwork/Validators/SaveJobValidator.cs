using FluentValidation;
using LebUpwor.core.Models;
using LebUpwork.Api.Resources.Save;
using System.Data;

namespace LebUpwork.Api.Validators
{
    public class SaveJobValidator : AbstractValidator<SaveJobResources>
    {
        public SaveJobValidator() 
        {
            RuleFor(a => a.User).NotNull();

            RuleFor(a => a.Title)
             .NotNull()
             .MinimumLength(8).WithMessage("Title must be at least 8 characters long")
             .MaximumLength(255).WithMessage("Title must not exceed 255 characters");

            RuleFor(a => a.Description)
            .NotNull()
            .MinimumLength(8).WithMessage("Description must be at least 20 characters long")
            .MaximumLength(255).WithMessage("Description must not exceed 528 characters");

            RuleFor(a => a.Offer)
             .NotNull()
             .WithMessage("Offer must not be null")
             .GreaterThan(5).WithMessage("Offer must be greater than 5$.");
        }

    }
}
