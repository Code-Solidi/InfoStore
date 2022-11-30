using OpenCqs;

using System;

namespace Bookmarks.UseCases.Commands
{
    public class UpdateBookmarkCommand : ICommand
    {
        public UpdateBookmarkCommand(Guid id, string title, string url, string description)
        {
            this.Id = id != Guid.Empty ? id : throw new ArgumentException($"{nameof(id)} is empty.", nameof(id));

            if (string.IsNullOrEmpty(title))
            {
                throw new ArgumentException($"{nameof(title)} is null or empty.", nameof(title));
            }

            this.Title = title;

            //if (string.IsNullOrEmpty(description))
            //{
            //    throw new ArgumentException($"{nameof(description)} is null or empty.", nameof(description));
            //}

            this.Description = description;

            if (string.IsNullOrEmpty(url))
            {
                throw new ArgumentException($"{nameof(url)} is null or empty.", nameof(url));
            }

            this.Url = url;
        }

        public Guid Id { get; init; }

        public string Title { get; init; }

        public string Url { get; init; }

        public string Description { get; init; }
    }
}