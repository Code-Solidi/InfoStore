using FluentValidation;

using System;

namespace InfoStore.Models.Validators
{
    public class ToDoModelValidator : AbstractValidator<ToDoModel>
    {
        public ToDoModelValidator()
        {
            const string textErrorMessage = "Todo's text must not be empty.";
            const string emailErrorMessage = "A valid email is required.";
            this.RuleFor(x => x.Text).NotNull().WithMessage(textErrorMessage).NotEmpty().WithMessage(textErrorMessage);
            this.RuleFor(x => x.DueDateTime).GreaterThanOrEqualTo(DateTime.Now);
            this.RuleFor(x => x.EMail)
                .NotNull().WithMessage(emailErrorMessage)
                .NotEmpty().WithMessage(emailErrorMessage)
                .When(x => x.Remind > 0)
                .When(x => x.TimeUnit != TimeUnit.None);
        }
    }
}