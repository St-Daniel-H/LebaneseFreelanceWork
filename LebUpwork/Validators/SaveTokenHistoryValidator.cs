using FluentValidation;
using LebUpwor.core.Models;
using LebUpwork.Api.Resources.Save;

namespace LebUpwork.Api.Validators
{
    public class SaveTokenHistoryValidator : AbstractValidator<SaveTokenHistoryResources>
    {
        //public required double AmountSent { get; set; }
        //public required Job Job { get; set; }
        //public User? Sender { get; set; }
        //public User? Receiver { get; set; }
       public SaveTokenHistoryValidator()
        {
            RuleFor(a => a.AmountSent)
                .NotEmpty().WithMessage("Amount must not be empty")
                .NotNull().WithMessage("Amount must not be null")
                .GreaterThan(5).WithMessage("Amount must be greater than 5");

            RuleFor(a => a.Job).NotNull().WithMessage("Job must not be null");
            RuleFor(a => a.Sender).NotNull().WithMessage("Sender must not be null");
            RuleFor(a => a.Receiver).NotNull().WithMessage("Receiver must not be null");
        }
    }
}
