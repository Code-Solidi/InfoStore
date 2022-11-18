using OpenCqs;

using System;

namespace InfoStore.UseCases.Commands
{
    public class AddToDoCommand : ICommand
    {
        public AddToDoCommand(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                throw new ArgumentException($"{nameof(text)} is null or empty.", nameof(text));
            }

            this.Text = text;
        }

        public string Text { get; init; }
    }
}