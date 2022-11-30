﻿using InfoStore.Data.Entities;
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
            return this.dbContext.Set<ToDo>().Select(x => new ToDoModel
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
        }
    }
}