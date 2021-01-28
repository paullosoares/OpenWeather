using MediatR;
using System;
using System.Threading.Tasks;
using Weather.Core.Notifications;
using Xunit;

namespace Weather.Tests.Integration.Notifications
{
    public class NotificationTests : TestIntegrationBase
    {
        private readonly IMediatorHandler _mediator;
        private readonly NotificationHandler _notifications;
        public NotificationTests()
        {
            CreateScope();
            _mediator = GetInstance<IMediatorHandler>();
            _notifications = (NotificationHandler)GetInstance<INotificationHandler<Notification>>();
        }

        [Fact(DisplayName = "Send Notification success")]
        [Trait("Categoria", "Notification Tests")]
        public async Task Notification_SendNotification_HasNotifications()
        {
            // Arrange
            var notification = new Notification(String.Empty, "Any notification");

            // Act
           await _mediator.SendEvent(notification);

           // Assert
           Assert.True(_notifications.HasNotifications());
           Assert.Single(_notifications.GetNotifications());
        }
    }
}
