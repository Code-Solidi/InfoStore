using InfoStore.UseCases.Queries;

using OpenCqs;

using System;
using System.Collections.Generic;
using System.Linq;

namespace InfoStore.Models
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

        public string Text { get; set; }

        public string EMail { get; set; }   // maybe overwritten in each reminder

        //public TimeUnit MinutesHoursDays { get; set; }

        //public uint Before { get; set; }

        //public uint Repeat { get; set; }

        public ToDoModel[] GetTodos()
        {
            var todosQuery = new GetToDosQuery { Group = this.Filter?.Group, Search = this.Filter?.Search };
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

    public class ToDoModel
    {
        public Guid Id { get; set; }

        public string Text { get; set; }

        public string EMail { get; set; }

        public TimeUnit MinutesHoursDays { get; set; }

        public uint Before { get; set; }

        public uint Repeat { get; set; }

        public enum TimeUnit
        {
            Minutes, Hours, Days
        }
    }
}