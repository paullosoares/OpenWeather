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
            switch ((int)response.StatusCode)
            {
                case 401:
                case 403:
                case 404:
                case 500:
                throw new CustomHttpRequestException(response.StatusCode);
                case 400:
                    return false;
            }

            response.EnsureSuccessStatusCode();
            return true;
        }
    }
}
