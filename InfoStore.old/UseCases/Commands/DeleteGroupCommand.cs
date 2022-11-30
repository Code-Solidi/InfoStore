using OpenCqs;

using System;

namespace InfoStore.UseCases.Commands
{
    public class DeleteGroupCommand : ICommand
    {
        public Guid Id { get; init; }

        public DeleteGroupCommand(Guid id)
        {
            this.Id = id != Guid.Empty ? id : throw new ArgumentException($"{nameof(id)} is empty.", nameof(id));
        }
    }
}