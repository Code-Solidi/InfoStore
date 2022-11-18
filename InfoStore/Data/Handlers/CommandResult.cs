using System;

namespace InfoStore.Data.Handlers
{
    public class CommandResult
    {
        public CommandResult(Exception exception = default)
        {
            this.Exception = exception;
        }

        public Exception Exception { get; }

        public bool IsSuccess => this.Exception == default;
    }
}