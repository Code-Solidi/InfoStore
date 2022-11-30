using OpenCqs;

using System;

namespace Notes.UseCases.Commands
{
    public class DeleteNoteCommand : ICommand
    {
        public DeleteNoteCommand(Guid id)
        {
            this.Id = id;
        }

        public Guid Id { get; init; }
    }
}