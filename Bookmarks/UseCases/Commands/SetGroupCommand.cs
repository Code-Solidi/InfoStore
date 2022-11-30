using OpenCqs;

using System;

namespace Bookmarks.UseCases.Commands
{
    public class SetGroupCommand : ICommand
    {
        public SetGroupCommand(Guid id, Guid groupId)
        {
            this.BoorkmarkId = id != Guid.Empty ? id : throw new InvalidOperationException("Bookmark id shouldn't be empty.");
            this.GroupId = groupId;
        }

        public Guid BoorkmarkId { get; init; }

        public Guid GroupId { get; init; }
    }
}