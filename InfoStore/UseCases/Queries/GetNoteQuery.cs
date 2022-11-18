using OpenCqs;

using System;

namespace InfoStore.UseCases.Queries
{
    public class GetNoteQuery : IQuery
    {
        public GetNoteQuery(Guid id)
        {
            this.NoteId = id != Guid.Empty ? id : throw new InvalidOperationException("Note id cannot be empty.");
        }

        public Guid NoteId { get; }
    }
}