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
    public class GetUpcommingToDosHandler : QueryHandlerBase<GetUpcommingToDosQuery, IEnumerable<ToDoModel>>
    {
        private readonly ToDoDbContext dbContext;

        public GetUpcommingToDosHandler(ToDoDbContext dbContext)
        {
            this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext), $"{nameof(dbContext)} is null.");
        }

        public override IEnumerable<ToDoModel> Handle(GetUpcommingToDosQuery query)
        {
            var deadline = DateTime.Now.NoSeconds();

            var todos = this.dbContext.Set<ToDo>().Where(x => x.Remind != 0);

            var inMinutes = todos.Where(x => x.TimeUnit == TimeUnit.Minutes
                && x.DueDateTime.AddMinutes(-x.Remind + x.Repeat * x.Notified) == deadline
                && deadline < x.DueDateTime);

            var inHours = todos.Where(x => x.TimeUnit == TimeUnit.Hours
                && x.DueDateTime.AddHours(-x.Remind + x.Repeat * x.Notified) <= deadline
                && deadline < x.DueDateTime);

            var inDays = todos.Where(x => x.TimeUnit == TimeUnit.Days
                && x.DueDateTime.AddDays(-x.Remind + x.Repeat * x.Notified) <= deadline
                && deadline < x.DueDateTime);

            var all = inMinutes.Union(inHours).Union(inDays);

            var result = all.Select(x => new ToDoModel
            {
                Id = x.Id,
                Text = x.Text,
                Done = x.Done,
                DueDateTime = x.DueDateTime,
                Remind = x.Remind,
                TimeUnit = x.TimeUnit,
                EMail = x.EMail,
                Repeat = x.Repeat,
                Notified = x.Notified
            }).AsNoTracking();

            return result;
        }
    }
}