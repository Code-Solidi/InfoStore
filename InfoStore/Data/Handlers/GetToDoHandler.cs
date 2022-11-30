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
            this.Add(new GetToDoExceptionHandler());
        }

        public override ToDoModel Handle(GetToDoQuery query)
        {
            var todo = this.dbContext.Set<ToDo>().AsNoTracking().SingleOrDefault(x => x.Id == query.Id);
            return new ToDoModel
            {
                Id = todo.Id,
                Text = todo.Text,
                DueDateTime = todo.DueDateTime,
                Remind = todo.Remind,
                TimeUnit = todo.TimeUnit,
                EMail = todo.EMail ?? query.Email,   // query.Email is the default
                Repeat = todo.Repeat,
                Notified = todo.Notified,
                Overdue = todo.Overdue
            };
        }
    }

    [Decorator]
    public class GetToDoExceptionHandler : QueryHandlerBase<GetToDoQuery, ToDoModel>
    {
        public override ToDoModel Handle(GetToDoQuery query)
        {
            try
            {
                return this.next?.Handle(query);
            }
            catch (DbUpdateException x)
            {
                return this.HandleException(x);
            }
        }

        private ToDoModel HandleException(Exception x)
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