using Bookmarks.UseCases.Queries;

using OpenCqs;

using System;
using System.Collections.Generic;
using System.Linq;

namespace Bookmarks.Models
{
    public class BookmarkIndexModel
    {
        private readonly IQueryHandler<GetBookmarksQuery, IEnumerable<BookmarkModel>> getBookmarks;
        private readonly IQueryHandler<GetGroupsForSelectQuery, IEnumerable<GroupSelect>> getGroups;

        public BookmarkIndexModel()
        {
        }

        public BookmarkIndexModel(IQueryHandler<GetBookmarksQuery, IEnumerable<BookmarkModel>> getBookmarks
            , IQueryHandler<GetGroupsForSelectQuery, IEnumerable<GroupSelect>> getGroups
            , BookmarkFilter filter = default)
        {
            this.Filter = filter;
            this.getBookmarks = getBookmarks ?? throw new ArgumentNullException(nameof(getBookmarks), $"{nameof(getBookmarks)} is null.");
            this.getGroups = getGroups ?? throw new ArgumentNullException(nameof(getGroups), $"{nameof(getGroups)} is null.");
        }

        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Url { get; set; }

        public string Description { get; set; }

        public Guid GroupId { get; set; }

        public BookmarkFilter Filter { get; init; }

        public GroupSelect[] GetGroups()
        {
            return this.getGroups?.Handle(new GetGroupsForSelectQuery()).ToArray() ?? Array.Empty<GroupSelect>();
        }

        public BookmarkModel[] GetBookmarks()
        {
            var bookmarksQuery = new GetBookmarksQuery { Group = this.Filter?.Group, Search = this.Filter?.Search };
            return this.getBookmarks?.Handle(bookmarksQuery).ToArray() ?? Array.Empty<BookmarkModel>();
        }

        public class GroupSelect
        {
            public Guid Id { get; set; }

            public string Name { get; set; }
        }

        public class BookmarkFilter
        {
            public BookmarkFilter(string group, string search)
            {
                this.Group = group;
                this.Search = search;
            }

            public string Search { get; init; }

            public string Group { get; init; }
        }
    }

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