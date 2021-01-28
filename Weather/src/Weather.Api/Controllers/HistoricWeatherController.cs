using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Weather.Core.Notifications;
using Weather.Domain.Services;

namespace Weather.Api.Controllers
{
    [Route("api/historic")]
    public class HistoricWeatherController : MainController
    {
        private readonly IHistoricWeatherService _historicWeatherService;
        public HistoricWeatherController(INotificationHandler<Notification> notifications, 
                                            IMediatorHandler mediatorHandler, 
                                            IHistoricWeatherService historicWeatherService) : base(notifications, mediatorHandler)
        {
            _historicWeatherService = historicWeatherService;
        }

        [HttpGet("{latitude}/{longitude}")]
        public async Task<IActionResult> GetHistoricByLatLon(double latitude, double longitude)
        {
            if (latitude == 0 || longitude == 0)
            {
                NotifyError($"Enter the {nameof(latitude)} and {nameof(longitude)} field.");
                return CustomResponse();
            }
            var response = await _historicWeatherService.GetHistoricByLatLon(latitude, longitude);
            return CustomResponse(response);
        }
    }
}
