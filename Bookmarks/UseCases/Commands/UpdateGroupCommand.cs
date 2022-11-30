using OpenCqs;

using System;

namespace Bookmarks.UseCases.Commands
{
    public class UpdateGroupCommand : ICommand
    {
        public UpdateGroupCommand(Guid id, string name, string description)
        {
            Description = description;
            Name = name;
            Id = id;
        }

        public Guid Id { get; init; }

        public string Name { get; init; }

        public string Description { get; init; }
    }
}