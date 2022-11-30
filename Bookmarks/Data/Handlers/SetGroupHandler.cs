using Bookmarks.Data.Entities;
using Bookmarks.UseCases.Commands;

using Microsoft.EntityFrameworkCore;

using OpenCqs;

using System;

namespace Bookmarks.Data.Handlers
{
    public class SetGroupHandler : CommandHandlerBase<SetGroupCommand, CommandResult>
    {
        private readonly ApplicationDbContext dbContext;

        public SetGroupHandler(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext), $"{nameof(dbContext)} is null.");
        }

        public override CommandResult Handle(SetGroupCommand command)
        {
            var bookmarks = this.dbContext.Set<Bookmark>();
            var bookmark = bookmarks.Find(command.BoorkmarkId);
            var group = this.dbContext.Set<Group>().Find(command.GroupId);
            if (bookmark.Group != group)
            {
                bookmark.Group = group;
                var result = this.dbContext.SaveChanges();
                return result != 1 ? throw new DbUpdateException("Failed") : new CommandResult();
            }

            return new CommandResult();
        }
    }
}