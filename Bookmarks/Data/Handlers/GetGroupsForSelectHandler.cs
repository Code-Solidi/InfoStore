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
    public class GetGroupsForSelectHandler : QueryHandlerBase<GetGroupsForSelectQuery, IEnumerable<BookmarkIndexModel.GroupSelect>>
    {
        private readonly ApplicationDbContext dbContext;

        public GetGroupsForSelectHandler(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext), $"{nameof(dbContext)} is null.");
        }

        public override IEnumerable<BookmarkIndexModel.GroupSelect> Handle(GetGroupsForSelectQuery query)
        {
            var groups = this.dbContext.Set<Group>();
            return groups.OrderBy(x => x.Name)
                .Select(x => new BookmarkIndexModel.GroupSelect { Id = x.Id, Name = x.Name })
                .AsNoTracking();
        }
    }
}