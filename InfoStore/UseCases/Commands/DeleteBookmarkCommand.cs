using OpenCqs;

using System;

namespace InfoStore.UseCases.Commands
{
    public class DeleteBookmarkCommand : ICommand
    {
        public DeleteBookmarkCommand(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; init; }
    }
}
