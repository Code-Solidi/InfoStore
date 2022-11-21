using OpenCqs;

namespace InfoStore.UseCases.Queries
{
    public class GetUpcommingToDosQuery : IQuery
    {
        public GetUpcommingToDosQuery(ushort notificationTreshold = 3)
        {
            this.NotificationTreshold = notificationTreshold;
        }
    
        public ushort NotificationTreshold { get; init; }
    }
}