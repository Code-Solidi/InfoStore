using OpenCqs;

using System;

namespace InfoStore.UseCases.Commands
{
    public class DeleteToDoCommand : ICommand
    {
        public DeleteToDoCommand(Guid id)
        {
            this.Id = id;
        }

        public Guid Id { get; init; }
    }
}