using Microsoft.EntityFrameworkCore;

using OpenCqs;

using System;

using ToDos.Data.Entities;
using ToDos.UseCases.Commands;

namespace ToDos.Data.Handlers
{
    public class DeleteToDoHandler : CommandHandlerBase<DeleteToDoCommand, CommandResult>
    {
        private readonly ToDoDbContext dbContext;

        public DeleteToDoHandler(ToDoDbContext dbContext)
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