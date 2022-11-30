using OpenCqs;

using System;

namespace Bookmarks.UseCases.Queries
{
    public class GetBookmarkQuery : IQuery
    {
        public GetBookmarkQuery(Guid id)
        {
            this.BookmarkId = id != Guid.Empty ? id : throw new InvalidOperationException("Bookmark id cannot be empty.");
        }

        public Guid BookmarkId { get; }
    }
}