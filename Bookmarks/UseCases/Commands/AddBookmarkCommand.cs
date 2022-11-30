using OpenCqs;

using System;

namespace Bookmarks.UseCases.Commands
{
    public class AddBookmarkCommand : ICommand
    {
        public AddBookmarkCommand(string title, string url, string description, Guid groupId)
        {
            this.Title = title;

            if (string.IsNullOrEmpty(url))
            {
                throw new ArgumentException($"{nameof(url)} is null or empty.", nameof(url));
            }

            this.Url = url;

            this.Description = description;
            this.GroupId = groupId;
        }

        public string Title { get; init; }

        public string Url { get; init; }

        public string Description { get; init; }

        public Guid GroupId { get; init; }
    }
}