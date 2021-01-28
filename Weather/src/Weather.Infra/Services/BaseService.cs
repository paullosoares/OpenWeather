using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Weather.Core.Exceptions;
using Weather.Core.Notifications;

namespace Weather.Infra.Services
{
    public abstract class BaseService
    {
        private readonly IMediatorHandler _mediator;

        protected BaseService(IMediatorHandler mediator)
        {
            _mediator = mediator;
        }

        protected void AddNotificationError(string key, string error)
        {
            _mediator.SendEvent(new Notification(key, error));
        }

        protected async Task<T> DeserializeResponseObject<T>(HttpResponseMessage responseMessage)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            return JsonSerializer.Deserialize<T>(await responseMessage.Content.ReadAsStringAsync(), options);
        }

        protected bool TryResponseError(HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode)
            {
                var errorContent = response.Content.ReadAsStringAsync().Result;
                AddNotificationError(string.Empty, errorContent);
            }

            return response.IsSuccessStatusCode;
        }
    }
}
