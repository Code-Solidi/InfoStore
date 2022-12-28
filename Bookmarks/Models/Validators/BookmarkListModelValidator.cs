using FluentValidation;

namespace Bookmarks.Models.Validators
{
    public class BookmarkListModelValidator : AbstractValidator<BookmarkListModel>
    {
        public BookmarkListModelValidator()
        {
            //this.RuleFor(x => x.Text)
            //    .NotNull().WithMessage("Please, enter a title.");
            this.RuleFor(x => x.Url)
                .NotNull().WithMessage("Please, enter a URL.")
                .Must(x => x.ValidateUri()).WithMessage("The URL entered is invalid.");
        }
    }
}