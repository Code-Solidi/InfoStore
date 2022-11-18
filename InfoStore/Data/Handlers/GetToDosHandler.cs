using CommonMark;

using InfoStore.Data.Entities;
using InfoStore.Models;
using InfoStore.UseCases.Queries;

using Microsoft.EntityFrameworkCore;

using OpenCqs;

using System;
using System.Collections.Generic;
using System.Linq;

namespace InfoStore.Data.Handlers
{
    public class GetToDosHandler : QueryHandlerBase<GetToDosQuery, IEnumerable<ToDoModel>>
    {
        private readonly ApplicationDbContext dbContext;

        public GetToDosHandler(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext), $"{nameof(dbContext)} is null.");
        }

        public override IEnumerable<ToDoModel> Handle(GetToDosQuery query)
        {
            var todos = this.dbContext.Set<ToDo>().AsQueryable();//.Include(x => x.Group);
            var queryResult = todos.AsQueryable();

            //if (!string.IsNullOrWhiteSpace(query.Group))
            //{
            //    queryResult = todos.Where(x => x.Group.Name == query.Group);
            //}

            //if (!string.IsNullOrWhiteSpace(query.Search))
            //{
            //    queryResult = todos.Where(x => EF.Functions.Like(x.Text, $"%{query.Search}%") || EF.Functions.Like(x.Url, $"%{query.Search}%"));
            //}

            return queryResult.Select(x => new ToDoModel
            {
                Id = x.Id,
                Text = x.Text
            });
        }
    }
}