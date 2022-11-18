using InfoStore.Data.Entities;
using InfoStore.Models;
using InfoStore.UseCases.Queries;

using OpenCqs;

using System;
using System.Linq;

namespace InfoStore.Data.Handlers
{
    public class GetNoteHandler : QueryHandlerBase<GetNoteQuery, NoteModel>
    {
        private readonly ApplicationDbContext dbContext;

        public GetNoteHandler(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext), $"{nameof(dbContext)} is null.");
        }

        public override NoteModel Handle(GetNoteQuery query)
        {
            var notes = this.dbContext.Set<Note>();
            return notes
                .Select(x => new NoteModel
                {
                    Id = x.Id,
                    Title = x.Title,
                    Content = x.Content
                })
                .SingleOrDefault(x => x.Id == query.NoteId);
        }
    }
}