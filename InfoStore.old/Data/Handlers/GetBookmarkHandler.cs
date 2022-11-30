using InfoStore.Data.Entities;
using InfoStore.Models;
using InfoStore.UseCases.Queries;

using Microsoft.EntityFrameworkCore;

using OpenCqs;

using System;
using System.Linq;

namespace InfoStore.Data.Handlers
{
    public class GetBookmarkHandler : QueryHandlerBase<GetBookmarkQuery, BookmarkModel>
    {
        private readonly ApplicationDbContext dbContext;

        public GetBookmarkHandler(ApplicationDbContext dbContext)
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