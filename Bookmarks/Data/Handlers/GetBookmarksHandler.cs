using Bookmarks.Data;
using Bookmarks.Data.Entities;
using Bookmarks.Models;
using Bookmarks.UseCases.Queries;

using CommonMark;

using Microsoft.EntityFrameworkCore;

using OpenCqs;

using System;
using System.Collections.Generic;
using System.Linq;

namespace Bookmarks.Data.Handlers
{
    public class GetBookmarksHandler : QueryHandlerBase<GetBookmarksQuery, IEnumerable<BookmarkModel>>
    {
        private readonly BookmarksDbContext dbContext;

        public GetBookmarksHandler(BookmarksDbContext dbContext)
        {
            this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext), $"{nameof(dbContext)} is null.");
        }

        public override IEnumerable<BookmarkModel> Handle(GetBookmarksQuery query)
        {
            var bookmarks = this.dbContext.Set<Bookmark>().AsQueryable().Include(x => x.Group).AsQueryable();

            if (!string.IsNullOrWhiteSpace(query.Group))
            {
                bookmarks = bookmarks.Where(x => x.Group.Name == query.Group);
            }
            else
            {
                bookmarks = bookmarks.Where(x => x.Group == default);
            }

            if (!string.IsNullOrWhiteSpace(query.Search))
            {
                bookmarks = bookmarks.Where(x => EF.Functions.Like(x.Title, $"%{query.Search}%") || EF.Functions.Like(x.Url, $"%{query.Search}%"));
            }

            return bookmarks.Select(x => new BookmarkModel
            {
                Id = x.Id,
                Title = x.Title,
                Url = x.Url,
                Description = CommonMarkConverter.Convert(x.Description, default),
                Group = x.Group.Name
            }).AsNoTracking();
        }
    }
}