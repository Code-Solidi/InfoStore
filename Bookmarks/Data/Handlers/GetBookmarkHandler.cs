using Bookmarks.Data;
using Bookmarks.Data.Entities;
using Bookmarks.Models;
using Bookmarks.UseCases.Queries;

using Microsoft.EntityFrameworkCore;

using OpenCqs;

using System;
using System.Linq;

namespace Bookmarks.Data.Handlers
{
    public class GetBookmarkHandler : QueryHandlerBase<GetBookmarkQuery, BookmarkModel>
    {
        private readonly BookmarksDbContext dbContext;

        public GetBookmarkHandler(BookmarksDbContext dbContext)
        {
            this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext), $"{nameof(dbContext)} is null.");
        }

        public override BookmarkModel Handle(GetBookmarkQuery query)
        {
            var groups = this.dbContext.Set<Group>();
            var bookmarks = this.dbContext.Set<Bookmark>();
            return bookmarks
                .Select(x => new BookmarkModel
                {
                    Id = x.Id,
                    Title = x.Title,
                    Url = x.Url,
                    Description = x.Description,
                    Group = x.Group.Name,
                    Groups = groups.Select(x => new GroupModel { Id = x.Id, Name = x.Name, Description = x.Description }).ToList()
                })
                .AsNoTracking()
                .SingleOrDefault(x => x.Id == query.BookmarkId);
        }
    }
}