using FluentValidation;

using ToDos.Models;

namespace Bookmarks.Models.Validators
{
    public class NoteListModelValidator : AbstractValidator<ToDoListModel>
    {
        public NoteListModelValidator()
        {
            this.RuleFor(x => x.Text).NotNull().WithMessage("ToDo text is required.");
        }
    }
}