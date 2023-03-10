using Bookmarks.Data;
using Bookmarks.Data.Entities;
using Bookmarks.Models;
using Bookmarks.UseCases.Queries;

using Microsoft.EntityFrameworkCore;

using OpenCqs;

using System;
using System.Collections.Generic;
using System.Linq;

namespace Bookmarks.Data.Handlers
{
    public class GetGroupsForSelectHandler : QueryHandlerBase<GetGroupsForSelectQuery, IEnumerable<BookmarkListModel.GroupSelect>>
    {
        private readonly BookmarksDbContext dbContext;

        public GetGroupsForSelectHandler(BookmarksDbContext dbContext)
        {
            this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext), $"{nameof(dbContext)} is null.");
        }

        public override IEnumerable<BookmarkListModel.GroupSelect> Handle(GetGroupsForSelectQuery query)
        {
            var groups = this.dbContext.Set<Group>().Include(x => x.Bookmarks).OrderBy(x => x.Name).AsQueryable();
            if (query.Nonempty)
            {
                groups = groups.Where(x => x.Bookmarks.Count > 0);
            }

            return groups.Select(x => new BookmarkListModel.GroupSelect { Id = x.Id, Name = x.Name }).AsNoTracking();
        }
    }
}