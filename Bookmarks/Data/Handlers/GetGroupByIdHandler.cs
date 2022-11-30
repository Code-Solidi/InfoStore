using Bookmarks.Data.Entities;
using Bookmarks.Models;
using Bookmarks.UseCases.Queries;

using Microsoft.EntityFrameworkCore;

using OpenCqs;

using System;
using System.Linq;

namespace Bookmarks.Data.Handlers
{
    public class GetGroupByIdHandler : QueryHandlerBase<GetGroupByIdQuery, GroupModel>
    {
        private readonly ApplicationDbContext dbContext;

        public GetGroupByIdHandler(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext), $"{nameof(dbContext)} is null.");
            this.Add(new GetGroupExceptionHandler());
        }

        public override GroupModel Handle(GetGroupByIdQuery query)
        {
            var group = this.dbContext.Set<Group>().AsNoTracking().SingleOrDefault(x => x.Id == query.Id);
            return new GroupModel
            {
                Id = group.Id,
                Name = group.Name,
                Description = group.Description,
                Bookmarks = this.dbContext.Set<Bookmark>()
                    .Where(x => x.Group.Id == query.Id)
                    .Select(x => new BookmarkModel { Id = x.Id, Url = x.Url, Description = x.Description }).ToList()
            };
        }
    }

    [Decorator]
    public class GetGroupExceptionHandler : QueryHandlerBase<GetGroupByIdQuery, GroupModel>
    {
        public override GroupModel Handle(GetGroupByIdQuery query)
        {
            try
            {
                return this.next?.Handle(query);
            }
            catch (DbUpdateException x)
            {
                return this.HandleException(x);
            }
            catch (NullReferenceException)
            {
                return new GroupModel { Id = query.Id };
            }

        }

        private GroupModel HandleException(Exception x)
        {
            if (x is DbUpdateException)
            {
                var message = x.InnerException?.Message ?? x.Message;
                // log message?
                //x = new Exception(message);
            }

            return default;
        }
    }
}