using Microsoft.EntityFrameworkCore;

using OpenCqs;

using System;
using System.Collections.Generic;
using System.Linq;

using ToDos.Code;
using ToDos.Data.Entities;
using ToDos.Models;
using ToDos.UseCases.Queries;

namespace ToDos.Data.Handlers
{
    public class GetOverdueToDosHandler : QueryHandlerBase<GetOverdueToDosQuery, IEnumerable<ToDoModel>>
    {
        private readonly ToDoDbContext dbContext;

        public GetOverdueToDosHandler(ToDoDbContext dbContext)
        {
            this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext), $"{nameof(dbContext)} is null.");
        }

        public override IEnumerable<ToDoModel> Handle(GetOverdueToDosQuery query)
        {
            var deadline = DateTime.Now.NoSeconds();
            return this.dbContext.Set<ToDo>().AsNoTracking()
                .Where(x => x.DueDateTime != DateTime.MinValue && x.DueDateTime < deadline && !x.Done && !x.Overdue)
                .Select(x => new ToDoModel
                {
                    Id = x.Id,
                    Text = x.Text,
                    Done = x.Done,
                    DueDateTime = x.DueDateTime,
                    Remind = x.Remind,
                    TimeUnit = x.TimeUnit,
                    EMail = x.EMail,
                    Repeat = x.Repeat
                });
        }
    }
}