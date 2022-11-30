using OpenCqs;

using System;

namespace InfoStore.UseCases.Queries
{
    public class GetGroupByIdQuery : IQuery
    {
        public GetGroupByIdQuery(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; init; }
    }
}