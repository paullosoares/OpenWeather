using Weather.Core.Data;
using Weather.Domain.Models;

namespace Weather.Domain.Repositories
{
    public interface IWeatherRepository : IRepository<OpenWeather>
    {
    }
}
