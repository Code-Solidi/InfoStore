using OpenCqs;

using System;

namespace ToDos.UseCases.Commands
{
    public class SetToDoOverdueCommand : ICommand
    {
        public SetToDoOverdueCommand(Guid id)
        {
            this.Id = id;
        }

        public Guid Id { get; init; }
    }
}