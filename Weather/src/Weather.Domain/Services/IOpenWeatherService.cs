using System.Threading.Tasks;
using Weather.Domain.Dto;

namespace Weather.Domain.Services
{
    public interface IOpenWeatherService
    {
        Task<OpenWeatherDto> GetTemperatureByCityName(string cityName);
        Task<OpenWeatherDto> GetTemperatureByLatLon(double latitude, double longitude);
    }
}
