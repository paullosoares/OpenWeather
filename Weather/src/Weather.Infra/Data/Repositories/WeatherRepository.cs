using Weather.Domain.Models;
using Weather.Domain.Repositories;

namespace Weather.Infra.Data.Repositories
{
    public class WeatherRepository : Repository<OpenWeather>, IWeatherRepository
    {
        public WeatherRepository(OpenWeatherContext context) : base(context)
        {
        }
    }
}
