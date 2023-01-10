using OpenCqs;

namespace Bookmarks.UseCases.Queries
{
    public class GetGroupsForSelectQuery : IQuery
    {
        public GetGroupsForSelectQuery(bool nonempty)
        {
            this.Nonempty = nonempty;
        }

        public bool Nonempty { get; init; }
    }
}