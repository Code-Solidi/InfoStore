using OpenCqs;

using System;

namespace InfoStore.UseCases.Queries
{
    public class GetToDoQuery : IQuery
    {
        public GetToDoQuery(Guid id, string email)
        {
            this.Email = email;
            this.Id = id;
        }

        public Guid Id { get; init; }

        public string Email { get; init; }
    }
}