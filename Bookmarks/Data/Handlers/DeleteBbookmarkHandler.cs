using Bookmarks.Data;
using Bookmarks.Data.Entities;
using Bookmarks.UseCases.Commands;

using Microsoft.EntityFrameworkCore;

using OpenCqs;

using System;

namespace Bookmarks.Data.Handlers
{
    public class DeleteBbookmarkHandler : CommandHandlerBase<DeleteBookmarkCommand, CommandResult>
    {
        private readonly BookmarksDbContext dbContext;

        public DeleteBbookmarkHandler(BookmarksDbContext dbContext)
        {
            this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext), $"{nameof(dbContext)} is null.");
            this.Add(new DeleteBookmarkExceptionCommandHandler());
        }

        public override CommandResult Handle(DeleteBookmarkCommand command)
        {
            var set = this.dbContext.Set<Bookmark>();
            var bookmark = set.Find(command.Id);
            set.Remove(bookmark);
            var result = this.dbContext.SaveChanges();
            if (result != 1)
            {
                throw new DbUpdateException("Boom!");
            }

            return new CommandResult();
        }
    }

    [Decorator]
    internal class DeleteBookmarkExceptionCommandHandler : CommandHandlerBase<DeleteBookmarkCommand, CommandResult>
    {
        public override CommandResult Handle(DeleteBookmarkCommand command)
        {
            try
            {
                return this.next?.Handle(command);
            }
            catch (DbUpdateException x)
            {
                return this.HandleException(x);
            }
        }

        private CommandResult HandleException(Exception x)
        {
            if (x is DbUpdateException)
            {
                var message = x.InnerException?.Message ?? x.Message;
                x = new Exception(message);
            }

            return new CommandResult(x);
        }
    }
}