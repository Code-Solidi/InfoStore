using Microsoft.EntityFrameworkCore;

using Notes.Data.Entities;
using Notes.UseCases.Commands;

using OpenCqs;

using System;
using System.Linq;

namespace Notes.Data.Handlers
{
    public class UpdateNoteHandler : CommandHandlerBase<UpdateNoteCommand, CommandResult>
    {
        private readonly NotesDbContext dbContext;

        public UpdateNoteHandler(NotesDbContext dbContext)
        {
            this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext), $"{nameof(dbContext)} is null.");
            this.Add(new UpdateNoteExceptionCommandHandler());
        }

        public override CommandResult Handle(UpdateNoteCommand command)
        {
            var notes = this.dbContext.Set<Note>();

            var note = notes.Find(command.Id);
            note.Title = command.Title;
            note.Content = command.Content;

            var result = this.dbContext.SaveChanges();
            return new[] { 0, 1 }.All(x => x != result) ? throw new DbUpdateException("Failed") : new CommandResult();
        }
    }

    public class UpdateNoteExceptionCommandHandler : CommandHandlerBase<UpdateNoteCommand, CommandResult>
    {
        public override CommandResult Handle(UpdateNoteCommand command)
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