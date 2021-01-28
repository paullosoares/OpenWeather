using System.Threading.Tasks;
using Weather.Domain.Dto;

namespace Weather.Domain.Services
{
    public interface IHistoricWeatherService
    {
        Task<HistoricWeatherDto> GetHistoricByLatLon(double latitude, double longitude);
    }
}
