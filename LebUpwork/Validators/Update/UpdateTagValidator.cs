using FluentValidation;
using LebUpwork.Api.Resources.Update;

namespace LebUpwork.Api.Validators.Update
{
    public class UpdateTagValidator : AbstractValidator<UpdateUserTags>
    {
        public UpdateTagValidator() {
          RuleFor(model => model.Tags)
        .Cascade(CascadeMode.Stop)
        .NotNull().WithMessage("The list of items is required.")
        .NotEmpty().WithMessage("The list of items cannot be empty.")
        .Must(tags => tags == null || tags.Count <= 5).WithMessage("The list of tags must contain at most 5 items.")
         .ForEach(itemRule =>
        {
            itemRule.NotNull().WithMessage("Each item in the list must not be null.");
            itemRule.NotEmpty().WithMessage("Each item in the list must not be empty.");
        });
        }
    }
}
