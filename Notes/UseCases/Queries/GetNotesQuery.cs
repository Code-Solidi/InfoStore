using OpenCqs;

namespace Notes.UseCases.Queries
{
    public class GetNotesQuery : IQuery
    {
        public string Group { get; set; }

        public string Search { get; set; }
    }
}