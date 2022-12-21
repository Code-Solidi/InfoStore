using System;
using System.Collections.Generic;

namespace Bookmarks.Data.Entities
{
    public class Bookmark
    {
        public Guid Id { get; set; }

        public string UserId { get; set; }

        public string Title { get; set; }

        public string Url { get; set; }

        public string Description { get; set; }

        public Group Group { get; set; }

        public ICollection<Tag> Tags { get; set; }
    }
}