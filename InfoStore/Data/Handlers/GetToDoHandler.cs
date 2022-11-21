using InfoStore.Data.Entities;
using InfoStore.Models;
using InfoStore.UseCases.Queries;

using Microsoft.EntityFrameworkCore;

using OpenCqs;

using System;
using System.Linq;

namespace InfoStore.Data.Handlers
{
    public class GetToDoHandler : QueryHandlerBase<GetToDoQuery, ToDoModel>
    {
        private readonly ApplicationDbContext dbContext;

        public GetToDoHandler(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext), $"{nameof(dbContext)} is null.");
        }

        public override ToDoModel Handle(GetToDoQuery query)
        {
            var todos = this.dbContext.Set<ToDo>().AsQueryable();
            var queryResult = todos.Where(x => x.Id == query.Id).ToArray();

            var result = queryResult.Select(x => new ToDoModel
            {
                Id = x.Id,
                Text = x.Text,
                DueDateTime = x.DueDateTime,
                Remind = x.Remind,
                TimeUnit = x.TimeUnit,
                EMail = x.EMail ?? query.Email,   // query.Email is the default
                Repeat = x.Repeat,
                Notified = x.Notified
            }).SingleOrDefault();

            return result;
        }
    }
}