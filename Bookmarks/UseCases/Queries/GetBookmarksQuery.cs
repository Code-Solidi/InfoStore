using OpenCqs;

using System;

namespace Bookmarks.UseCases.Queries
{
    public class GetBookmarksQuery : IQuery
    {
        public GetBookmarksQuery(string group, string search = default)
        {
            this.Group = group;
            this.Search = search;
        }

        public string Group { get; init; }

        public string Search { get; init; }
    }
}