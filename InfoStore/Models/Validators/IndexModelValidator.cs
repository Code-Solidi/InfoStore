using FluentValidation;

namespace InfoStore.Models.Validators
{
    public class IndexModelValidator : AbstractValidator<BookmarkIndexModel>
    {
        public IndexModelValidator()
        {
            //this.RuleFor(x => x.Text)
            //    .NotNull().WithMessage("Please, enter a title.");
            this.RuleFor(x => x.Url)
                .NotNull().WithMessage("Please, enter a URL.")
                .Must(x => x.ValidateUri()).WithMessage("The URL entered is invalid.");
        }
    }
}