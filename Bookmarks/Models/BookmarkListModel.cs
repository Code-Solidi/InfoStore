using Bookmarks.UseCases.Queries;

using OpenCqs;

using System;
using System.Collections.Generic;
using System.Linq;

namespace Bookmarks.Models
{
    public class BookmarkListModel
    {
        private readonly IQueryHandler<GetBookmarksQuery, IEnumerable<BookmarkModel>> getBookmarks;
        private readonly IQueryHandler<GetGroupsForSelectQuery, IEnumerable<GroupSelect>> getGroups;

        public BookmarkListModel()
        {
        }

        public BookmarkListModel(IQueryHandler<GetBookmarksQuery, IEnumerable<BookmarkModel>> getBookmarks
            , IQueryHandler<GetGroupsForSelectQuery, IEnumerable<GroupSelect>> getGroups
            , BookmarkFilter filter = default)
        {
            this.getBookmarks = getBookmarks ?? throw new ArgumentNullException(nameof(getBookmarks), $"{nameof(getBookmarks)} is null.");
            this.getGroups = getGroups ?? throw new ArgumentNullException(nameof(getGroups), $"{nameof(getGroups)} is null.");
            this.Filter = filter;
        }

        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Url { get; set; }

        public string Description { get; set; }

        public Guid GroupId { get; set; }

        public BookmarkFilter Filter { get; init; }

        public GroupSelect[] GetGroups(bool nonempty = false)
        {
            return this.getGroups?.Handle(new GetGroupsForSelectQuery(nonempty)).ToArray() ?? Array.Empty<GroupSelect>();
        }

        public BookmarkModel[] GetBookmarks(string group = default)
        {
            var bookmarksQuery = new GetBookmarksQuery(group, this.Filter?.Search);
            return this.getBookmarks?.Handle(bookmarksQuery).ToArray() ?? Array.Empty<BookmarkModel>();
        }

        public class GroupSelect
        {
            public Guid Id { get; set; }

            public string Name { get; set; }
        }

        public class BookmarkFilter
        {
            public BookmarkFilter(string search)
            {
                this.Search = search;
            }

            public string Search { get; init; }
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