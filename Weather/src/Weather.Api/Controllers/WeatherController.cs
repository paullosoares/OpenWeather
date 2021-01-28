using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using MediatR;
using Weather.Core.Notifications;
using Weather.Domain.Services;

namespace Weather.Api.Controllers
{
    [Route("api/weather")]
    public class WeatherController : MainController
    {
        private readonly IOpenWeatherService _openWeatherService;


        public WeatherController(IOpenWeatherService openWeatherService,
                                INotificationHandler<Notification> notifications,
                                IMediatorHandler mediatorHandler) : base(notifications, mediatorHandler)
        {
            _openWeatherService = openWeatherService;
        }

        [HttpGet("byCityName")]
        public async Task<IActionResult> Get([FromQuery] string cityName)
        {
            if (string.IsNullOrEmpty(cityName))
            {
                NotifyError($"Enter the {nameof(cityName)} field.");
                return CustomResponse();
            }

            var response = await _openWeatherService.GetTemperatureByCityName(cityName);

            return CustomResponse(response);
        }

        [HttpGet("byLatLon")]
        public async Task<IActionResult> Get([FromQuery] double latitude, [FromQuery] double longitude)
        {
            if (latitude == 0.0 || longitude == 0.0)
            {
                NotifyError($"Enter the {nameof(latitude)} and {nameof(longitude)} field.");
                return CustomResponse();
            }
            var response = await _openWeatherService.GetTemperatureByLatLon(latitude, longitude);

            return CustomResponse(response);
        }
    }
}
