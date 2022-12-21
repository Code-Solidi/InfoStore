using System;

using ToDos.Models;

namespace ToDos.Data.Entities
{
    public class ToDo
    {
        public Guid Id { get; set; }

        public string UserId { get; set; }

        public string Text { get; set; }

        public bool Done { get; set; }

        public DateTime DueDateTime { get; set; }

        public string EMail { get; set; }

        public TimeUnit TimeUnit { get; set; }

        public ushort Remind { get; set; }

        public ushort Repeat { get; set; }

        public ushort Notified { get; set; }

        public bool Overdue { get; set; }
    }
}