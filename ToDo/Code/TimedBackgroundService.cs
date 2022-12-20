using Microsoft.Extensions.Hosting;

using OpenCqs;

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using ToDos.Data.Handlers;
using ToDos.Models;
using ToDos.UseCases.Commands;
using ToDos.UseCases.Queries;

namespace ToDos.Code
{
    // https://stackoverflow.com/questions/53727850/how-to-run-backgroundservice-on-a-timer-in-asp-net-core-2-1
    public class TimedBackgroundService : BackgroundService
    {
        private readonly IQueryHandler<GetOverdueToDosQuery, IEnumerable<ToDoModel>> getOverdueToDos;
        private readonly IQueryHandler<GetUpcommingToDosQuery, IEnumerable<ToDoModel>> getUpcommingToDos;
        readonly ICommandHandler<SetToDoNotifiedCommand, CommandResult> setToDoNotified;
        readonly ICommandHandler<SetToDoOverdueCommand, CommandResult> setToDoOverdue;

        public TimedBackgroundService(IQueryHandler<GetUpcommingToDosQuery, IEnumerable<ToDoModel>> getUpcommingToDos
            , IQueryHandler<GetOverdueToDosQuery, IEnumerable<ToDoModel>> getOverdueToDos
            , ICommandHandler<SetToDoNotifiedCommand, CommandResult> setToDoNotified
            , ICommandHandler<SetToDoOverdueCommand, CommandResult> setToDoOverdue)
        {
            this.setToDoOverdue = setToDoOverdue ?? throw new ArgumentNullException(nameof(setToDoOverdue), $"{nameof(setToDoOverdue)} is null.");
            this.setToDoNotified = setToDoNotified ?? throw new ArgumentNullException(nameof(setToDoNotified), $"{nameof(setToDoNotified)} is null.");
            this.getOverdueToDos = getOverdueToDos ?? throw new ArgumentNullException(nameof(getOverdueToDos), $"{nameof(getOverdueToDos)} is null.");
            this.getUpcommingToDos = getUpcommingToDos ?? throw new ArgumentNullException(nameof(getUpcommingToDos), $"{nameof(getUpcommingToDos)} is null.");
        }

        private enum MessageType
        {
            None, Upcomming, Overdue
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var interval = TimeSpan.FromSeconds(60);
            using (var timer = new PeriodicTimer(interval))
            {
                while (!stoppingToken.IsCancellationRequested && await timer.WaitForNextTickAsync(stoppingToken))
                {
                    // Alternative to PeriodicTimer (available in .NET Core 2.1).
                    // REMARK: Calls would successively be delayed a little bit this way.
                    //await Task.Delay(interval, stoppingToken);

                    await ExecuteTaskAsync();
                }
            }
        }

        private async Task ExecuteTaskAsync()
        {
            var upcomming = this.getUpcommingToDos.Handle(new GetUpcommingToDosQuery());
            var overdue = this.getOverdueToDos.Handle(new GetOverdueToDosQuery());

            Console.WriteLine($"[{DateTime.Now.NoSeconds()}]: {new string('-', 80)}");
            foreach (var item in upcomming)
            {
                this.SendMessage(item, MessageType.Upcomming);
            }

            foreach (var item in overdue)
            {
                this.SendMessage(item, MessageType.Overdue);
            }

            await Task.CompletedTask;
        }

        private void SendMessage(ToDoModel item, MessageType messageType)
        {
            switch (messageType)
            {
                case MessageType.Upcomming:
                    this.setToDoNotified.Handle(new SetToDoNotifiedCommand(item.Id));
                    break;

                case MessageType.Overdue:
                    this.setToDoOverdue.Handle(new SetToDoOverdueCommand(item.Id));
                    break;
            }

            Console.WriteLine($"[{DateTime.Now.NoSeconds()}]: Sending {messageType} message to '{item.Text}' due {item.DueDateTime} with reminder {item.Remind} {item.TimeUnit} ({item.Repeat}).");
        }
    }
}