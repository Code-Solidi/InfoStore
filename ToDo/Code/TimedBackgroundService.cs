using Microsoft.Extensions.Hosting;

using NuGet.Protocol.Plugins;

using OpenCqs;

using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Reflection;
using System.Text;
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

            this.SendEmailAsync(messageType, item).GetAwaiter().GetResult();
        }


        async Task SendEmailAsync(MessageType type, ToDoModel item)
        {
            var message = new MailMessage { From = new MailAddress("office@codesolidi.com") };
            var subject = Enum.GetName(typeof(MessageType), type);

            message.Subject = $"ToDo {subject}";
            message.IsBodyHtml = false;
            message.Body = $"{item.Text}";
            message.SubjectEncoding = Encoding.UTF8;
            message.BodyEncoding = Encoding.UTF8;
            message.To.Add(item.EMail);

            using (var client = new SmtpClient())
            {
#if DEBUG
                client.EnableSsl = false;
                client.Host = "127.0.0.1";
                client.Port = 25;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
#else
                client.EnableSsl = true;
                client.Host = "codesolidi.com";
                client.Port = 587;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.Credentials = new NetworkCredential("office@codesolidi.com", "d261f332");
#endif
                await client.SendMailAsync(message);
            }
        }
    }
}