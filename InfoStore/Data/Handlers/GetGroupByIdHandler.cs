using InfoStore.Data.Entities;
using InfoStore.Models;
using InfoStore.UseCases.Queries;

using OpenCqs;

using System;
using System.Linq;

namespace InfoStore.Data.Handlers
{
    public class GetGroupByIdHandler : QueryHandlerBase<GetGroupByIdQuery, GroupModel>
    {
        private readonly ApplicationDbContext dbContext;

        public GetGroupByIdHandler(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext), $"{nameof(dbContext)} is null.");
        }

        public override GroupModel Handle(GetGroupByIdQuery query)
        {
            var groups = this.dbContext.Set<Group>();
            var result = groups.Where(x => x.Id == query.Id).Select(x => new GroupModel
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                Bookmarks = this.dbContext.Set<Bookmark>()
                    .Where(x => x.Group.Id == query.Id)
                    .Select(x => new BookmarkModel { Id = x.Id, Url = x.Url, Description = x.Description }).ToList()
            });

            return result.Any() ? result.SingleOrDefault() : new GroupModel();
        }
    }
}