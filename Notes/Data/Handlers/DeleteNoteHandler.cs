using Microsoft.EntityFrameworkCore;

using Notes.Data.Entities;
using Notes.UseCases.Commands;

using OpenCqs;

using System;

namespace Notes.Data.Handlers
{
    public class DeleteNoteHandler : CommandHandlerBase<DeleteNoteCommand, CommandResult>
    {
        private readonly NotesDbContext dbContext;

        public DeleteNoteHandler(NotesDbContext dbContext)
        {
            this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext), $"{nameof(dbContext)} is null.");
            this.Add(new DeleteNoteExceptionCommandHandler());
        }

        public override CommandResult Handle(DeleteNoteCommand command)
        {
            var set = this.dbContext.Set<Note>();
            var note = set.Find(command.Id);
            set.Remove(note);
            var result = this.dbContext.SaveChanges();
            if (result != 1)
            {
                throw new DbUpdateException("Boom!");
            }

            return new CommandResult();
        }
    }

    [Decorator]
    internal class DeleteNoteExceptionCommandHandler : CommandHandlerBase<DeleteNoteCommand, CommandResult>
    {
        public override CommandResult Handle(DeleteNoteCommand command)
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