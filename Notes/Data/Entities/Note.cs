using System;

namespace Notes.Data.Entities
{
    public class Note
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }
    }
}
