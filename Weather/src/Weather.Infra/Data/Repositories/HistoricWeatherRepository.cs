using Weather.Domain.Models;
using Weather.Domain.Repositories;

namespace Weather.Infra.Data.Repositories
{
    public class HistoricWeatherRepository : Repository<HistoricWeather>, IHistoricWeatherRepository
    {
        public HistoricWeatherRepository(OpenWeatherContext context) : base(context)
        {
        }
    }
}
