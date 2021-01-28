using MediatR;
using System.Threading.Tasks;

namespace Weather.Core.Notifications
{
    public interface IMediatorHandler
    {
        Task SendEvent<T>(T @event) where T : INotification;
    }
}
