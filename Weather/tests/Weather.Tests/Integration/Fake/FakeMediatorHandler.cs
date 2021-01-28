using MediatR;
using System.Threading.Tasks;
using Weather.Core.Notifications;

namespace Weather.Tests.Integration.Fake
{
    public class FakeMediatorHandler : IMediatorHandler
    {
        private readonly IMediator _mediator;
        public FakeMediatorHandler(IMediator mediator)
        {
            _mediator = mediator;
        }
        public async Task SendEvent<T>(T @event) where T : INotification
        {
            await _mediator.Send(@event);
        }
    }
}
