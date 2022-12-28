using OpenCqs;

using System;

namespace ToDos.UseCases.Commands
{
    public class AddToDoCommand : ICommand
    {
        public AddToDoCommand(string text, string userId, string email)
        {
            if (string.IsNullOrEmpty(text))
            {
                throw new ArgumentException($"{nameof(text)} is null or empty.", nameof(text));
            }

            if (string.IsNullOrEmpty(userId))
            {
                throw new ArgumentException($"{nameof(userId)} is null or empty.", nameof(userId));
            }

            if (string.IsNullOrEmpty(email))
            {
                throw new ArgumentException($"{nameof(email)} is null or empty.", nameof(email));
            }

            this.Text = text;
            this.UserId = userId;
            this.Email = email;
        }

        public string Text { get; init; }

        public string UserId { get; set; }

        public string Email { get; set; }
    }
}