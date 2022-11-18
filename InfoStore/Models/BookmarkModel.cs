using System;
using System.Collections.Generic;

namespace InfoStore.Models
{
    public class BookmarkModel
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Url { get; set; }

        public string Description { get; set; }

        public string Group { get; set; }

        public IEnumerable<GroupModel> Groups { get; set; }
    }
}