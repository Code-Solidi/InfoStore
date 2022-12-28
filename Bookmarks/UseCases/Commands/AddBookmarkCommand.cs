using OpenCqs;

using System;

namespace Bookmarks.UseCases.Commands
{
    public class AddBookmarkCommand : ICommand
    {
        public AddBookmarkCommand(string title, string url, string description, Guid groupId, string userId)
        {
            if (string.IsNullOrEmpty(url))
            {
                throw new ArgumentException($"{nameof(url)} is null or empty.", nameof(url));
            }

            if (string.IsNullOrEmpty(userId))
            {
                throw new ArgumentException($"{nameof(userId)} is null or empty.", nameof(userId));
            }

            this.UserId = userId;
            this.Title = title;
            this.Url = url;
            this.Description = description;
            this.GroupId = groupId;
        }

        public string UserId { get; init; }

        public string Title { get; init; }

        public string Url { get; init; }

        public string Description { get; init; }

        public Guid GroupId { get; init; }
    }
}