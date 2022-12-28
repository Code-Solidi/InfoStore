using FluentValidation;

using Notes.Models;

namespace Bookmarks.Models.Validators
{
    public class NoteListModelValidator : AbstractValidator<NoteListModel>
    {
        public NoteListModelValidator()
        {
            this.RuleFor(x => x.Title).NotNull().WithMessage("Note title is required.");
        }
    }
}