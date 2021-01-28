using MediatR;
using System.Threading.Tasks;

namespace Weather.Core.Notifications
{
    public class MediatorHandler : IMediatorHandler
    {
        private readonly IMediator _mediator;

        public MediatorHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task SendEvent<T>(T @event) where T : INotification
        {
            await _mediator.Publish(@event);
        }
    }
}
