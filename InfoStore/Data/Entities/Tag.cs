using System;
using System.Collections.Generic;

namespace InfoStore.Data.Entities
{
    public class Tag
    {
        public Guid Id { get; set; }

        public string Text { get; set; }

        public ICollection<Bookmark> Bookmarks { get; set; }
    }
}