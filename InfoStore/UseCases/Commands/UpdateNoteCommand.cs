using OpenCqs;

using System;

namespace InfoStore.UseCases.Commands
{
    public class UpdateNoteCommand : ICommand
    {
        public UpdateNoteCommand(Guid id, string title, string content)
        {
            this.Id = id != Guid.Empty ? id : throw new ArgumentException($"{nameof(id)} is empty.", nameof(id));

            if (string.IsNullOrEmpty(title))
            {
                throw new ArgumentException($"{nameof(title)} is null or empty.", nameof(title));
            }

            this.Title = title;

            if (string.IsNullOrEmpty(content))
            {
                throw new ArgumentException($"{nameof(content)} is null or empty.", nameof(content));
            }

            this.Content = content;
        }

        public Guid Id { get; init; }

        public string Title { get; init; }

        public string Content { get; init; }
    }
}