using System;

namespace InfoStore.Models
{
    public class ToDoModel
    {
        public Guid Id { get; set; }

        public int Index { get; set; }

        public string Text { get; set; }

        public bool Done { get; set; }

        public DateTime DueDateTime { get; set; }

        public string EMail { get; set; }

        public TimeUnit TimeUnit { get; set; }

        public ushort Remind { get; set; }

        public ushort Repeat { get; set; }

        public ushort Notified { get; set; }
    }

    public enum TimeUnit
    {
        None, Minutes, Hours, Days
    }

    //public enum ToDoStatus
    //{
    //    Indeterminate, InProgress, Complete, Pending, Overdue
    //}
}