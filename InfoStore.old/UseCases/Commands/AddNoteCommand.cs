using OpenCqs;

using System;

namespace InfoStore.UseCases.Commands
{
    public class AddNoteCommand : ICommand
    {
        public AddNoteCommand(string title, string content)
        {
            if (string.IsNullOrEmpty(title))
            {
                throw new ArgumentException($"{nameof(title)} is null or empty.", nameof(title));
            }

            this.Title = title;
            this.Content = content;
        }

        public string Title { get; init; }

        public string Content { get; init; }
    }
}