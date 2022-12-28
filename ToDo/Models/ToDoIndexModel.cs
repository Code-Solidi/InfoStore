using OpenCqs;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

using ToDos.UseCases.Queries;

namespace ToDos.Models
{
    public class ToDoListModel
    {
        private IQueryHandler<GetToDosQuery, IEnumerable<ToDoModel>> getTodos;

        public ToDoFilter Filter { get; init; }

        public ToDoListModel()
        {
        }

        public ToDoListModel(IQueryHandler<GetToDosQuery, IEnumerable<ToDoModel>> getTodos, string email, ToDoFilter filter = default)
        {
            this.getTodos = getTodos;
            this.EMail = email;
            this.Filter = filter;
        }

        public bool IsValid => this.getTodos != default;

        public string Text { get; set; }

        public string EMail { get; set; }   // maybe overwritten in each reminder

        public ToDoModel[] GetToDos(string userId)
        {
            var todosQuery = new GetToDosQuery(userId);// { EMail = this.EMail, Group = this.Filter?.Group, Search = this.Filter?.Search };
            return this.getTodos?.Handle(todosQuery).ToArray() ?? Array.Empty<ToDoModel>();
        }

        public class ToDoFilter
        {
            public ToDoFilter(string group, string search)
            {
                this.Group = group;
                this.Search = search;
            }

            public string Search { get; init; }

            public string Group { get; init; }
        }
    }
}