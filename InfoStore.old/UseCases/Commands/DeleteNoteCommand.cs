using OpenCqs;

using System;

namespace InfoStore.UseCases.Commands
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