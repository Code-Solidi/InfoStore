using InfoStore.Data;
using InfoStore.Data.Entities;
using InfoStore.UseCases.Commands;

using Microsoft.EntityFrameworkCore;

using OpenCqs;

using System;
using System.Linq;

namespace InfoStore.Data.Handlers
{
    public class UpdateGroupHandler : CommandHandlerBase<UpdateGroupCommand, CommandResult>
    {
        private readonly ApplicationDbContext dbContext;

        public UpdateGroupHandler(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext), $"{nameof(dbContext)} is null.");
            Add(new UpdateGroupExceptionCommandHandler());
        }

        public override CommandResult Handle(UpdateGroupCommand command)
        {
            var set = dbContext.Set<Group>();
            var group = set.SingleOrDefault(x => x.Id == command.Id);
            if (group == default)
            {
                throw new DbUpdateException($"Cannot find a group with Id='{command.Id}'. Cannot update it.");
            }

            group.Name = command.Name;
            group.Description = command.Description;

            set.Update(group);
            var result = this.dbContext.SaveChanges();
            return new[] { 0, 1 }.All(x => x != result) ? throw new DbUpdateException("Failed") : new CommandResult();
        }
    }


    [Decorator]
    internal class UpdateGroupExceptionCommandHandler : CommandHandlerBase<UpdateGroupCommand, CommandResult>
    {
        public override CommandResult Handle(UpdateGroupCommand command)
        {
            try
            {
                return next?.Handle(command);
            }
            catch (DbUpdateException x)
            {
                return HandleException(x);
            }
        }

        private CommandResult HandleException(Exception x)
        {
            if (x is DbUpdateException)
            {
                var message = x.InnerException?.Message ?? x.Message;
                message = message.StartsWith("Cannot insert duplicate key")
                    ? "Already added."
                    : x.InnerException.Message;
                x = new Exception(message);
            }

            return new CommandResult(x);
        }
    }

}