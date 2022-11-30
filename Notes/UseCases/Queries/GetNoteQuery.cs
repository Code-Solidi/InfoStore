using OpenCqs;

using System;

namespace Notes.UseCases.Queries
{
    public class GetNoteQuery : IQuery
    {
        public GetNoteQuery(Guid id)
        {
            this.Id = id != Guid.Empty ? id : throw new InvalidOperationException("Note id cannot be empty.");
        }

        public Guid Id { get; }
    }
}