using FluentValidation;

namespace InfoStore.Models.Validators
{
    public class GroupModelValidator : AbstractValidator<GroupModel>
    {
        public GroupModelValidator()
        {
            this.RuleFor(x => x.Name).NotNull().WithMessage("A valid non-null, non-empty name required.");
        }
    }
}