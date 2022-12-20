using OpenCqs;

namespace ToDos.UseCases.Queries
{
    public class GetToDosQuery : IQuery
    {
        public string EMail { get; set; }

        public string Group { get; set; }

        public string Search { get; set; }
    }
}