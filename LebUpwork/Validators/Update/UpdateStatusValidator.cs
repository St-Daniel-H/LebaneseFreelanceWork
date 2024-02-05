using FluentValidation;
using LebUpwork.Api.Resources.Update;

namespace LebUpwork.Api.Validators.Update
{
    public class UpdateStatusValidator: AbstractValidator<UpdateUserStatusResources>
    {
        public UpdateStatusValidator()
        {
            RuleFor(a => a.Status).NotNull()
            .NotEmpty()
            .MaximumLength(528);
        }
    }
}
