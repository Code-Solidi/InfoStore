using Bookmarks.Data.Entities;
using Bookmarks.UseCases.Commands;

using Microsoft.EntityFrameworkCore;

using OpenCqs;

using System;
using System.Linq;

namespace Bookmarks.Data.Handlers
{
    public class UpdateBookmarkHandler : CommandHandlerBase<UpdateBookmarkCommand, CommandResult>
    {
        private readonly BookmarksDbContext dbContext;

        public UpdateBookmarkHandler(BookmarksDbContext dbContext)
        {
            this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext), $"{nameof(dbContext)} is null.");
            this.Add(new UpdateBookmarkExceptionCommandHandler());
        }

        public override CommandResult Handle(UpdateBookmarkCommand command)
        {
            var bookmarks = this.dbContext.Set<Bookmark>();

            var bookmark = bookmarks.Find(command.Id);
            bookmark.Title = command.Title;
            bookmark.Url = command.Url;
            bookmark.Description = command.Description;

            var result = this.dbContext.SaveChanges();
            return new[] { 0, 1 }.All(x => x != result) ? throw new DbUpdateException("Failed") : new CommandResult();
        }
    }

    public class UpdateBookmarkExceptionCommandHandler : CommandHandlerBase<UpdateBookmarkCommand, CommandResult>
    {
        public override CommandResult Handle(UpdateBookmarkCommand command)
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