using InfoStore.Code;
using InfoStore.Data.Entities;
using InfoStore.Models;
using InfoStore.UseCases.Queries;
using OpenCqs;

using System;
using System.Collections.Generic;
using System.Linq;

namespace InfoStore.Data.Handlers
{
    public class GetUpcommingToDosHandler : QueryHandlerBase<GetUpcommingToDosQuery, IEnumerable<ToDoModel>>
    {
        private readonly ApplicationDbContext dbContext;

        public GetUpcommingToDosHandler(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext), $"{nameof(dbContext)} is null.");
        }

        public override IEnumerable<ToDoModel> Handle(GetUpcommingToDosQuery query)
        {
            var deadline = DateTime.Now.NoSeconds();

            var todos = this.dbContext.Set<ToDo>().Where(x => x.Remind != 0);

            var inMinutes = todos/*this.dbContext.Set<ToDo>()*/.Where(x => x.TimeUnit == TimeUnit.Minutes 
                && x.DueDateTime.AddMinutes(-x.Remind) == deadline 
                && deadline < x.DueDateTime 
                && x.Notified < query.NotificationTreshold);
            
            var inHours = this.dbContext.Set<ToDo>().Where(x => x.TimeUnit == TimeUnit.Hours 
                && x.DueDateTime.AddHours(-x.Remind) <= deadline 
                && deadline < x.DueDateTime
                && x.Notified < query.NotificationTreshold);

            var inDays = this.dbContext.Set<ToDo>().Where(x => x.TimeUnit == TimeUnit.Days 
                && x.DueDateTime.AddDays(-x.Remind) <= deadline 
                && deadline < x.DueDateTime 
                && x.Notified < query.NotificationTreshold);

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
            });

            return result;
        }
    }
}