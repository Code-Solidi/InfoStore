using InfoStore.Models;

using OpenCqs;

using System;

namespace InfoStore.UseCases.Commands
{
    public class UpdateToDoCommand : ICommand
    {
        public UpdateToDoCommand(Guid id, string text, bool done, DateTime dueDateTime, string email, ushort remind, TimeUnit timeUnit, ushort repeat)
        {
            this.Id = id;
            this.Text = text;
            this.Done = done;
            this.DueDateTime = dueDateTime;
            this.Email = email;
            this.Remind = remind;
            this.TimeUnit = timeUnit;
            this.Repeat = repeat;
        }

        public Guid Id { get; init; }

        public string Text { get; init; }

        public bool Done { get; init; }

        public DateTime DueDateTime { get; init; }

        public string Email { get; init; }

        public ushort Remind { get; init; }

        public TimeUnit TimeUnit { get; init; }

        public ushort Repeat { get; init; }
    }
}