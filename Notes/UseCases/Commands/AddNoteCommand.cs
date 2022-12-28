using OpenCqs;

using System;

namespace Notes.UseCases.Commands
{
    public class AddNoteCommand : ICommand
    {
        public AddNoteCommand(string title, string content, string userId)
        {
            if (string.IsNullOrEmpty(title))
            {
                throw new ArgumentException($"{nameof(title)} is null or empty.", nameof(title));
            }

            if (string.IsNullOrEmpty(userId))
            {
                throw new ArgumentException($"{nameof(userId)} is null or empty.", nameof(userId));
            }

            this.Title = title;
            this.Content = content;
            this.UserId = userId;
        }

        public string Title { get; init; }

        public string Content { get; init; }

        public string UserId { get; init; }
    }
}