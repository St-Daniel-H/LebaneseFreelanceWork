using FluentValidation;
using LebUpwork.Api.Resources.Save;

namespace LebUpwork.Api.Validators
{
    public class SaveCashOutHistoryValidator : AbstractValidator<SaveCashOutHistoryResources>
    {
        public SaveCashOutHistoryValidator() {
            RuleFor(a => a.User).NotNull();

            RuleFor(a => a.Amount)
            .NotNull()
            .WithMessage("Amount must not be null")
            .GreaterThan(50).WithMessage("Amount must be greater than 50$.");

        }
    }
}
