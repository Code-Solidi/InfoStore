using InfoStore.Data.Entities;
using InfoStore.UseCases.Commands;

using Microsoft.EntityFrameworkCore;

using OpenCqs;

using System;

namespace InfoStore.Data.Handlers
{
    public class DeleteToDoHandler : CommandHandlerBase<DeleteToDoCommand, CommandResult>
    {
        private readonly ApplicationDbContext dbContext;

        public DeleteToDoHandler(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext), $"{nameof(dbContext)} is null.");
            this.Add(new DeleteToDoExceptionCommandHandler());
        }

        public override CommandResult Handle(DeleteToDoCommand command)
        {
            var set = this.dbContext.Set<ToDo>();
            var todo = set.Find(command.Id);
            set.Remove(todo);
            var result = this.dbContext.SaveChanges();
            if (result != 1)
            {
                throw new DbUpdateException("Boom!");
            }

            return new CommandResult();
        }
    }

    [Decorator]
    internal class DeleteToDoExceptionCommandHandler : CommandHandlerBase<DeleteToDoCommand, CommandResult>
    {
        public override CommandResult Handle(DeleteToDoCommand command)
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