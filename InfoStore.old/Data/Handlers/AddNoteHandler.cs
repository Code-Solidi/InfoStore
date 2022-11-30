using InfoStore.Data.Entities;
using InfoStore.UseCases.Commands;

using Microsoft.EntityFrameworkCore;

using OpenCqs;

using System;

namespace InfoStore.Data.Handlers
{
    public class AddNoteHandler : CommandHandlerBase<AddNoteCommand, CommandResult>
    {
        private readonly ApplicationDbContext dbContext;

        public AddNoteHandler(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext), $"{nameof(dbContext)} is null.");
            this.Add(new AddNoteExceptionCommandHandler());
        }

        public override CommandResult Handle(AddNoteCommand command)
        {
            var notes = this.dbContext.Set<Note>();

            notes.Add(new Note {  Title = command.Title, Content = command.Content });
            var result = this.dbContext.SaveChanges();

            return result != 1 ? throw new DbUpdateException("Failed") : new CommandResult();
        }
    }

    [Decorator]
    internal class AddNoteExceptionCommandHandler : CommandHandlerBase<AddNoteCommand, CommandResult>
    {
        public override CommandResult Handle(AddNoteCommand command)
        {
            try
            {
                return this.next?.Handle(command);
            }
            catch (DbUpdateException x)
            {
                return this.HandleException(x, command.Title);
            }
        }

        private CommandResult HandleException(Exception x, string title)
        {
            if (x is DbUpdateException)
            {
                var message = x.InnerException.Message.StartsWith("Cannot insert duplicate key")
                    ? $"Already added: {title}"
                    : x.InnerException.Message;
                x = new Exception(message);
            }

            return new CommandResult(x);
        }
    }
}
