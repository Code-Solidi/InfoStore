using FluentValidation;

namespace Bookmarks.Models.Validators
{
    public class BookmarkModelValidator : AbstractValidator<BookmarkModel>
    {
        public BookmarkModelValidator()
        {
            this.RuleFor(x => x.Title).NotEmpty().WithMessage("Please, enter a title.");
            this.RuleFor(x => x.Url)
                .NotNull().WithMessage("Please, enter a URL.")
                .Must(x => x.ValidateUri()).WithMessage("The URL entered is invalid.");
        }
    }
}