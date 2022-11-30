using System;
using System.Collections.Generic;

namespace Bookmarks.Models
{
    public class GroupModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public ICollection<BookmarkModel> Bookmarks { get; set; }

        public IEnumerable<GroupModel> Groups { get; set; }
    }
}