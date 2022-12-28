using OpenCqs;

using System;

namespace Bookmarks.UseCases.Commands
{
    public class AddGroupCommand : ICommand
    {
        public AddGroupCommand(string name, string description, string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                throw new ArgumentException($"{nameof(userId)} is null or empty.", nameof(userId));
            }

            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException($"{nameof(name)} is null or empty.", nameof(name));
            }

            this.UserId = userId;
            Name = name;
            Description = description;
        }

        public string UserId { get; init; }

        public string Name { get; init; }

        public string Description { get; init; }
    }
}