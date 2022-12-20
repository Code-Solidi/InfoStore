using OpenCqs;

namespace ToDos.UseCases.Queries
{
    public class GetOverdueToDosQuery : IQuery
    {
        public GetOverdueToDosQuery(ushort notificationTreshold = 3)
        {
            this.NotificationTreshold = notificationTreshold;
        }

        public ushort NotificationTreshold { get; }
    }
}
