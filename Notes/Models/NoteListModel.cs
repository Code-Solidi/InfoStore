using Notes.UseCases.Queries;

using OpenCqs;

using System;
using System.Collections.Generic;
using System.Linq;

namespace Notes.Models
{
    public class NoteListModel
    {
        private readonly IQueryHandler<GetNotesQuery, IEnumerable<NoteModel>> getNotes;

        public NoteListModel()
        {
        }

        public NoteListModel(IQueryHandler<GetNotesQuery, IEnumerable<NoteModel>> getNotes, NotesFilter filter = default)
        {
            this.Filter = filter;
            this.getNotes = getNotes;
        }

        public string Title { get; set; }

        public string Content { get; set; }

        public NotesFilter Filter { get; }

        public NoteModel[] GetNotes()
        {
            var notesQuery = new GetNotesQuery { Group = this.Filter?.Group, Search = this.Filter?.Search };
            return this.getNotes?.Handle(notesQuery).ToArray() ?? Array.Empty<NoteModel>();
        }

        public class NotesFilter
        {
            public string Group { get; internal set; }

            public string Search { get; internal set; }
        }
    }

    public class NoteModel
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }
    }
}