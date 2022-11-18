using CommonMark;

using InfoStore.Data.Entities;
using InfoStore.Models;
using InfoStore.UseCases.Queries;

using Microsoft.EntityFrameworkCore;

using OpenCqs;

using System;
using System.Collections.Generic;
using System.Linq;

namespace InfoStore.Data.Handlers
{
    public class GetBookmarksHandler : QueryHandlerBase<GetBookmarksQuery, IEnumerable<BookmarkModel>>
    {
        private readonly ApplicationDbContext dbContext;

        public GetBookmarksHandler(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext), $"{nameof(dbContext)} is null.");
        }

        public override IEnumerable<BookmarkModel> Handle(GetBookmarksQuery query)
        {
            var bookmarks = this.dbContext.Set<Bookmark>().AsQueryable().Include(x => x.Group);
            var queryResult = bookmarks.AsQueryable();

            if (!string.IsNullOrWhiteSpace(query.Group))
            {
                queryResult = bookmarks.Where(x => x.Group.Name == query.Group);
            }

            if (!string.IsNullOrWhiteSpace(query.Search))
            {
                queryResult = bookmarks.Where(x => EF.Functions.Like(x.Title, $"%{query.Search}%") || EF.Functions.Like(x.Url, $"%{query.Search}%"));
            }

            return queryResult.Select(x => new BookmarkModel
            {
                Id = x.Id,
                Title = x.Title,
                Url= x.Url,
                Description = CommonMarkConverter.Convert(x.Description, default),
                Group = x.Group.Name
            });
        }
    }
}