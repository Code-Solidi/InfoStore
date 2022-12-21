using Bookmarks.Data;
using Bookmarks.Data.Entities;
using Bookmarks.UseCases.Commands;

using Microsoft.EntityFrameworkCore;

using OpenCqs;

using System;

namespace Bookmarks.Data.Handlers
{
    public class DeleteGroupHandler : CommandHandlerBase<DeleteGroupCommand, CommandResult>
    {
        private readonly BookmarksDbContext dbContext;

        public DeleteGroupHandler(BookmarksDbContext dbContext)
        {
            this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext), $"{nameof(dbContext)} is null.");
            this.Add(new DeleteGroupExceptionCommandHandler());
        }

        public override CommandResult Handle(DeleteGroupCommand command)
        {
            var set = this.dbContext.Set<Group>();
            var group = set.Find(command.Id);
            set.Remove(group);
            var result = this.dbContext.SaveChanges();
            if (result != 1)
            {
                throw new DbUpdateException("Boom!");
            }

            return new CommandResult();
        }
    }

    [Decorator]
    internal class DeleteGroupExceptionCommandHandler : CommandHandlerBase<DeleteGroupCommand, CommandResult>
    {
        public override CommandResult Handle(DeleteGroupCommand command)
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