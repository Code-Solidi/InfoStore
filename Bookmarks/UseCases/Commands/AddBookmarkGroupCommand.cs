using OpenCqs;

namespace Bookmarks.UseCases.Commands
{
    public class AddGroupCommand : ICommand
    {
        public AddGroupCommand(string name, string description)
        {
            Name = name;
            Description = description;
        }

        public string Name { get; init; }

        public string Description { get; init; }
    }
}