using OpenCqs;

using System;

namespace ToDos.UseCases.Commands
{
    public class SetToDoNotifiedCommand : ICommand
    {
        public SetToDoNotifiedCommand(Guid id)
        {
            this.Id = id;
        }

        public Guid Id { get; init; }
    }
}