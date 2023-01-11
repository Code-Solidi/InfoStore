using OpenCqs;

using System;

namespace Bookmarks.UseCases.Commands
{
    public class SetGroupCommand : ICommand
    {

        public SetGroupCommand(Guid id, Guid groupId)
        {
            this.BoorkmarkId = id != Guid.Empty ? id : throw new InvalidOperationException("Bookmark id shouldn't be empty.");
            this.GroupId = groupId != Guid.Empty ? groupId : throw new InvalidOperationException("Group id shouldn't be empty."); ;
        }

        public Guid BoorkmarkId { get; init; }

        public Guid GroupId { get; init; }
    }
}