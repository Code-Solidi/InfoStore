using System.Collections.Generic;
using System;

namespace Bookmarks.Data.Entities
{
    public class Group
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public ICollection<Bookmark> Bookmarks { get; set; }
    }
}
