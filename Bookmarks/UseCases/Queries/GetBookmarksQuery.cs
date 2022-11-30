using OpenCqs;

namespace Bookmarks.UseCases.Queries
{
    public class GetBookmarksQuery : IQuery
    {
        public string Group { get; set; }

        public string Search { get; set; }
    }
}