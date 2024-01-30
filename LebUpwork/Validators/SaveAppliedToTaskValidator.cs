using FluentValidation;
using LebUpwork.Api.Resources.Save;

namespace LebUpwork.Api.Validators
{
    public class SaveAppliedToTaskValidator : AbstractValidator<SaveAppliedToTaskResources>

    {
        public SaveAppliedToTaskValidator()
        {
            RuleFor(a => a.User).NotNull();
            RuleFor(a => a.Job).NotNull();
        }
    }
}
