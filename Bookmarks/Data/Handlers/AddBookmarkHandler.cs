using Bookmarks.Data;
using Bookmarks.Data.Entities;
using Bookmarks.UseCases.Commands;

using Microsoft.EntityFrameworkCore;

using OpenCqs;

using System;

namespace Bookmarks.Data.Handlers
{
    public class AddBookmarkHandler : CommandHandlerBase<AddBookmarkCommand, CommandResult>
    {
        private readonly BookmarksDbContext dbContext;

        public AddBookmarkHandler(BookmarksDbContext dbContext)
        {
            this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext), $"{nameof(dbContext)} is null.");
            this.Add(new AddBookmarkExceptionCommandHandler());
        }

        public override CommandResult Handle(AddBookmarkCommand command)
        {
            var group = this.dbContext.Set<Group>().Find(command.GroupId);
            var bookmarks = this.dbContext.Set<Bookmark>();

            bookmarks.Add(new Bookmark { Url = command.Url, Description = command.Description, Group = group, UserId = command.UserId });
            var result = this.dbContext.SaveChanges();
            return result != 1 ? throw new DbUpdateException("Failed") : new CommandResult();
        }
    }

    [Decorator]
    internal class AddBookmarkExceptionCommandHandler : CommandHandlerBase<AddBookmarkCommand, CommandResult>
    {
        public override CommandResult Handle(AddBookmarkCommand command)
        {
            try
            {
                return this.next?.Handle(command);
            }
            catch (DbUpdateException x)
            {
                return this.HandleException(x, command.Url);
            }
        }

        private CommandResult HandleException(Exception x, string url)
        {
            if (x is DbUpdateException)
            {
                var message = x.InnerException.Message.StartsWith("Cannot insert duplicate key")
                    ? $"Already added: <a href={url} target='_blank'>{url}</a>"
                    : x.InnerException.Message;
                x = new Exception(message);
            }

            return new CommandResult(x);
        }
    }
}
