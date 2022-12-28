using Microsoft.EntityFrameworkCore;

using OpenCqs;

using System;
using System.Collections.Generic;
using System.Linq;

using ToDos.Data.Entities;
using ToDos.Models;
using ToDos.UseCases.Queries;

namespace ToDos.Data.Handlers
{
    public class GetToDosHandler : QueryHandlerBase<GetToDosQuery, IEnumerable<ToDoModel>>
    {
        private readonly ToDoDbContext dbContext;

        public GetToDosHandler(ToDoDbContext dbContext)
        {
            this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext), $"{nameof(dbContext)} is null.");
        }

        public override IEnumerable<ToDoModel> Handle(GetToDosQuery query)
        {
            return this.dbContext.Set<ToDo>().Where(x => x.UserId == query.UserId).Select(x => new ToDoModel
            {
                Id = x.Id,
                UserId = x.UserId,
                Text = x.Text,
                Done = x.Done,
                DueDateTime = x.DueDateTime,
                Remind = x.Remind,
                TimeUnit = x.TimeUnit,
                EMail = x.EMail,
                Repeat = x.Repeat,
                Notified = x.Notified
            }).AsNoTracking();
        }
    }
}