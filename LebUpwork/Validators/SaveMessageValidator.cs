using FluentValidation;
using LebUpwor.core.Models;
using LebUpwork.Api.Resources.Save;

namespace LebUpwork.Api.Validators
{
    public class SaveMessageValidator : AbstractValidator<SaveMessageResources>
    {
        SaveMessageValidator() {
            //     public string Text { get; set; }
            //public User? Sender { get; set; }
            //public User? Receiver { get; set; }

            RuleFor(a => a.Sender).NotNull().WithMessage("Sender must not be null");
            RuleFor(a => a.Receiver).NotNull().WithMessage("Receiver must not be null");
            RuleFor(a => a.Text)
             .NotNull().WithMessage("Message must not be null")
             .MaximumLength(1028).WithMessage("Maximum characters 1028");
        }
    }
}
