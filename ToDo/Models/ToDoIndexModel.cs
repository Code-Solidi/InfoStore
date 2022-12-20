using OpenCqs;

using System;
using System.Collections.Generic;
using System.Linq;

using ToDos.UseCases.Queries;

namespace ToDos.Models
{
    public class ToDoIndexModel
    {
        private IQueryHandler<GetToDosQuery, IEnumerable<ToDoModel>> getTodos;

        public ToDoFilter Filter { get; init; }

        public ToDoIndexModel()
        {
        }

        public ToDoIndexModel(IQueryHandler<GetToDosQuery, IEnumerable<ToDoModel>> getTodos, string email, ToDoFilter filter = default)
        {
            this.getTodos = getTodos;
            this.EMail = email;
            this.Filter = filter;
        }

        public bool IsValid => this.getTodos != default;

        public string Text { get; set; }

        public string EMail { get; set; }   // maybe overwritten in each reminder

        public ToDoModel[] GetToDos()
        {
            var todosQuery = new GetToDosQuery { EMail = this.EMail, Group = this.Filter?.Group, Search = this.Filter?.Search };
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