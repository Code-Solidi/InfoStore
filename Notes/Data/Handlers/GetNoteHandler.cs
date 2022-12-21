using Microsoft.EntityFrameworkCore;

using Notes.Data.Entities;
using Notes.Models;
using Notes.UseCases.Queries;

using OpenCqs;

using System;
using System.Linq;

namespace Notes.Data.Handlers
{
    public class GetNoteHandler : QueryHandlerBase<GetNoteQuery, NoteModel>
    {
        private readonly NotesDbContext dbContext;

        public GetNoteHandler(NotesDbContext dbContext)
        {
            this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext), $"{nameof(dbContext)} is null.");
            this.Add(new GetNoteExceptionHandler());
        }

        public override NoteModel Handle(GetNoteQuery query)
        {
            var note = this.dbContext.Set<Note>().AsNoTracking().SingleOrDefault(x => x.Id == query.Id);
            return new NoteModel
            {
                Id = note.Id,
                Title = note.Title,
                Content = note.Content
            };
        }
    }

    [Decorator]
    public class GetNoteExceptionHandler : QueryHandlerBase<GetNoteQuery, NoteModel>
    {
        public override NoteModel Handle(GetNoteQuery query)
        {
            try
            {
                return this.next?.Handle(query);
            }
            catch (DbUpdateException x)
            {
                return this.HandleException(x);
            }
        }

        private NoteModel HandleException(Exception x)
        {
            if (x is DbUpdateException)
            {
                var message = x.InnerException?.Message ?? x.Message;
                // log message?
                //x = new Exception(message);
            }

            return default;
        }
    }
}