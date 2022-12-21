using Microsoft.EntityFrameworkCore;

using OpenCqs;

using System;
using System.Linq;

using ToDos.Data.Entities;
using ToDos.UseCases.Commands;

namespace ToDos.Data.Handlers
{
    public class SetToDoCompleteHandler : CommandHandlerBase<SetToDoCheckedComplete, CommandResult>
    {
        private readonly ToDoDbContext dbContext;

        public SetToDoCompleteHandler(ToDoDbContext dbContext)
        {
            this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext), $"{nameof(dbContext)} is null.");
            this.Add(new SetToDoCompleteExceptionCommandHandler());
        }

        public override CommandResult Handle(SetToDoCheckedComplete command)
        {
            var todos = this.dbContext.Set<ToDo>();
            var todo = todos.Find(command.Id);
            if (todo != default)
            {
                todo.Done = command.Checked;
                todos.Update(todo);
                var result = this.dbContext.SaveChanges();
                return new[] { 0, 1 }.All(x => x != result) ? throw new DbUpdateException("Failed") : new CommandResult();
            }

            throw new Exception("Not found");
        }

        [Decorator]
        internal class SetToDoCompleteExceptionCommandHandler : CommandHandlerBase<SetToDoCheckedComplete, CommandResult>
        {
            public override CommandResult Handle(SetToDoCheckedComplete command)
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
