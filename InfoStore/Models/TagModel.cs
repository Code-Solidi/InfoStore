using System;
using System.Collections.Generic;

namespace InfoStore.Models
{
    public class TagModel
    {
        public Guid Id { get; set; }

        public string Text { get; set; }

        public ICollection<BookmarkIndexModel> Bookmarks { get; set; }
    }
}