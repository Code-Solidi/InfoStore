using OpenCqs;

using System;

namespace ToDos.UseCases.Queries
{
    public class GetToDosQuery : IQuery
    {
        public GetToDosQuery(string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                throw new ArgumentException($"{nameof(userId)} is null or empty.", nameof(userId));
            }

            this.UserId = userId;
        }

        //public string EMail { get; set; }

        //public string Group { get; set; }

        //public string Search { get; set; }
        public string UserId { get; init; }
    }
}