using CommonMark;

using Microsoft.EntityFrameworkCore;

using Notes.Data.Entities;
using Notes.Models;
using Notes.UseCases.Queries;

using OpenCqs;

using System;
using System.Collections.Generic;
using System.Linq;

namespace Notes.Data.Handlers
{
    public class GetNotesHandler : QueryHandlerBase<GetNotesQuery, IEnumerable<NoteModel>>
    {
        private readonly ApplicationDbContext dbContext;

        public GetNotesHandler(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext), $"{nameof(dbContext)} is null.");
        }

        public override IEnumerable<NoteModel> Handle(GetNotesQuery query)
        {
            var notes = this.dbContext.Set<Note>().AsQueryable();
            var queryResult = notes.AsQueryable();

            if (!string.IsNullOrWhiteSpace(query.Group))
            {
                //queryResult = notes.Where(x => x.Group.Name == query.Group);
            }

            if (!string.IsNullOrWhiteSpace(query.Search))
            {
                //queryResult = notes.Where(x => EF.Functions.Like(x.Text, $"%{query.Search}%") || EF.Functions.Like(x.Url, $"%{query.Search}%"));
            }

            return queryResult.Select(x => new NoteModel
            {
                Id = x.Id,
                Title = x.Title,
                Content = CommonMarkConverter.Convert(x.Content, default)
            }).AsNoTracking();
        }
    }
}