using Bookmarks.Data;
using Bookmarks.Data.Entities;
using Bookmarks.UseCases.Commands;

using Microsoft.EntityFrameworkCore;

using OpenCqs;

using System;

namespace Bookmarks.Data.Handlers
{
    public class AddGroupHandler : CommandHandlerBase<AddGroupCommand, CommandResult>
    {
        private readonly ApplicationDbContext dbContext;

        public AddGroupHandler(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext), $"{nameof(dbContext)} is null.");
            this.Add(new AddGroupExceptionCommandHandler());
        }

        public override CommandResult Handle(AddGroupCommand command)
        {
            var set = this.dbContext.Set<Group>();
            set.Add(new Group { Name = command.Name, Description = command.Description });
            var result = this.dbContext.SaveChanges();
            if (result != 1)
            {
                throw new DbUpdateException("Boom!");
            }

            return new CommandResult();
        }
    }

    [Decorator]
    internal class AddGroupExceptionCommandHandler : CommandHandlerBase<AddGroupCommand, CommandResult>
    {
        public override CommandResult Handle(AddGroupCommand command)
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
                if (x.InnerException.Message.StartsWith("Cannot insert duplicate key"))
                {
                    message = "Already added.";
                }

                if (x.InnerException.Message.StartsWith("Cannot insert the value NULL"))
                {
                    message = "A substantial piece of data is missing.";
                }

                x = new Exception(message);
            }

            return new CommandResult(x);
        }
    }
}