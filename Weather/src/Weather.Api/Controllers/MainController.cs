using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections.Generic;
using System.Linq;
using MediatR;
using Weather.Core.Notifications;

namespace Weather.Api.Controllers
{
    public abstract class MainController : Controller
    {
        private readonly IMediatorHandler _mediatorHandler;
        private readonly NotificationHandler _notifications;

        protected MainController(INotificationHandler<Notification> notifications, IMediatorHandler mediatorHandler)
        {
            _mediatorHandler = mediatorHandler;
            _notifications = (NotificationHandler)notifications;
        }

        protected ActionResult CustomResponse(object result = null)
        {
            if (IsValidOperation())
            {
                return Ok(result);
            }

            return BadRequest(new ValidationProblemDetails(new Dictionary<string, string[]>
            {
                { "Messages",_notifications.GetNotifications().Select(m => m.Value).ToArray() }
            }));
        }

        protected ActionResult CustomResponse(ModelStateDictionary modelState)
        {
            var errors = modelState.Values.SelectMany(e => e.Errors);

            foreach (var error in errors)
            {
                NotifyError(error.ErrorMessage);
            }

            return CustomResponse();
        }

        protected void NotifyError(string error)
        {
            _mediatorHandler.SendEvent(new Notification(string.Empty, error));
        }

        protected bool IsValidOperation()
        {
            return (!_notifications.HasNotifications());
        }
    }
}
