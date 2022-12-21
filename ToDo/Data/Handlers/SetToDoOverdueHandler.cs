using Microsoft.EntityFrameworkCore;

using OpenCqs;

using System;
using System.Linq;

using ToDos.Data.Entities;
using ToDos.UseCases.Commands;

namespace ToDos.Data.Handlers
{
    public class SetToDoOverdueHandler : CommandHandlerBase<SetToDoOverdueCommand, CommandResult>
    {
        private readonly ToDoDbContext dbContext;

        public SetToDoOverdueHandler(ToDoDbContext dbContext)
        {
            this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext), $"{nameof(dbContext)} is null.");
            this.Add(new SetToDoOverdueExceptionCommandHandler());
        }

        public override CommandResult Handle(SetToDoOverdueCommand command)
        {
            var todos = this.dbContext.Set<ToDo>();
            var todo = todos.Find(command.Id);
            if (todo != default)
            {
                todo.Overdue = true;
                todos.Update(todo);
                var result = this.dbContext.SaveChanges();
                return new[] { 0, 1 }.All(x => x != result) ? throw new DbUpdateException("Failed") : new CommandResult();
            }

            throw new Exception("Not found");
        }

        [Decorator]
        internal class SetToDoOverdueExceptionCommandHandler : CommandHandlerBase<SetToDoOverdueCommand, CommandResult>
        {
            public override CommandResult Handle(SetToDoOverdueCommand command)
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
}
