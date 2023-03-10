using Microsoft.EntityFrameworkCore;

using OpenCqs;

using System;
using System.Linq;

using ToDos.Code;
using ToDos.Data.Entities;
using ToDos.UseCases.Commands;

namespace ToDos.Data.Handlers
{
    public class UpdateToDoHandler : CommandHandlerBase<UpdateToDoCommand, CommandResult>
    {
        private readonly ToDoDbContext dbContext;

        public UpdateToDoHandler(ToDoDbContext dbContext)
        {
            this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext), $"{nameof(dbContext)} is null.");
            this.Add(new UpdateToDoExceptionCommandHandler());
        }

        public override CommandResult Handle(UpdateToDoCommand command)
        {
            var todos = this.dbContext.Set<ToDo>();
            var todo = todos.Find(command.Id);
            if (todo != default)
            {
                todo.Text = command.Text;
                todo.Done = command.Done;
                todo.DueDateTime = command.DueDateTime.NoSeconds();
                todo.EMail = command.Email;
                todo.Remind = command.Remind;
                todo.TimeUnit = command.TimeUnit;
                todo.Repeat = command.Repeat;
                todo.Notified = 0;
                todo.Overdue = false;
                //todo.Notified = todo.DueDateTime != command.DueDateTime || todo.Repeat != command.Repeat || todo.Remind != command.Remind 
                //    ? (ushort)0
                //    : todo.Notified;

                todos.Update(todo);
                var result = this.dbContext.SaveChanges();
                return new[] { 0, 1 }.All(x => x != result) ? throw new DbUpdateException("Failed") : new CommandResult();
            }

            throw new Exception("Not found");
        }
    }

    [Decorator]
    internal class UpdateToDoExceptionCommandHandler : CommandHandlerBase<UpdateToDoCommand, CommandResult>
    {
        public override CommandResult Handle(UpdateToDoCommand command)
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
