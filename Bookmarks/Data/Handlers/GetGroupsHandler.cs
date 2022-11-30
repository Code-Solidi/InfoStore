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
    public class GetGroupsHandler : QueryHandlerBase<GetGroupsQuery, IEnumerable<GroupModel>>
    {
        private readonly ApplicationDbContext dbContext;

        public GetGroupsHandler(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext), $"{nameof(dbContext)} is null.");
        }

        public override IEnumerable<GroupModel> Handle(GetGroupsQuery query)
        {
            var groups = this.dbContext.Set<Group>();
            return groups.Select(x => new GroupModel
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                Bookmarks = this.dbContext.Set<Bookmark>()
                    .Where(g => g.Group.Id == x.Id)
                    .Select(x => new BookmarkModel { Id = x.Id, Url = x.Url, Description = x.Description }).ToList()
            }).AsNoTracking();
        }
    }
}