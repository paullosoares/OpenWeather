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
                                IMediatorHandler mediatorHandler)  : base(notifications, mediatorHandler)
        {
            _openWeatherService = openWeatherService;
        }

        [HttpGet("{cityName}")]
        public async Task<IActionResult> GetTemperatureByCityName(string cityName)
        {
            if (string.IsNullOrEmpty(cityName))
            {
                NotifyError($"Enter the {nameof(cityName)} field.");
                return CustomResponse();
            }

            var response = await _openWeatherService.GetTemperatureByCityName(cityName);

            return CustomResponse(response);
        }

        [HttpGet("{latitude}/{longetude}")]
        public async Task<IActionResult> GetTemperatureByLatLon(double latitude, double longetude)
        {
            if (latitude == 0 || longetude == 0)
            {
                NotifyError($"Enter the {nameof(latitude)} and {nameof(longetude)} field.");
                return CustomResponse();
            }
            var response = await _openWeatherService.GetTemperatureByLatLon(latitude, longetude);
            
            return CustomResponse(response);
        }
    }
}
