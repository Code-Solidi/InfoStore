using Microsoft.EntityFrameworkCore;

using OpenCqs;

using System;

using ToDos.Data;
using ToDos.Data.Entities;
using ToDos.UseCases.Commands;

namespace ToDos.Data.Handlers
{
    public class AddToDoHandler : CommandHandlerBase<AddToDoCommand, CommandResult>
    {
        private readonly ApplicationDbContext dbContext;

        public AddToDoHandler(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext), $"{nameof(dbContext)} is null.");
            this.Add(new AddToDoExceptionCommandHandler());
        }

        public override CommandResult Handle(AddToDoCommand command)
        {
            var todos = this.dbContext.Set<ToDo>();

            todos.Add(new ToDo { Text = command.Text });
            var result = this.dbContext.SaveChanges();

            return result != 1 ? throw new DbUpdateException("Failed") : new CommandResult();
        }
    }

    [Decorator]
    internal class AddToDoExceptionCommandHandler : CommandHandlerBase<AddToDoCommand, CommandResult>
    {
        public override CommandResult Handle(AddToDoCommand command)
        {
            try
            {
                return this.next?.Handle(command);
            }
            catch (DbUpdateException x)
            {
                return this.HandleException(x, command.Text);
            }
        }

        private CommandResult HandleException(Exception x, string title)
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
