using OpenCqs;

namespace InfoStore.UseCases.Queries
{
    public class GetToDosQuery : IQuery
    {
        public string Group { get; set; }

        public string Search { get; set; }
    }
}