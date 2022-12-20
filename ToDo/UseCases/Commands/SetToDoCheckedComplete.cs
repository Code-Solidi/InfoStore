using OpenCqs;

using System;

namespace ToDos.UseCases.Commands
{
    public class SetToDoCheckedComplete : ICommand
    {
        public SetToDoCheckedComplete(Guid id, bool @checked)
        {
            this.Id = id;
            this.Checked = @checked;
        }

        public Guid Id { get; init; }

        public bool Checked { get; init; }
    }
}