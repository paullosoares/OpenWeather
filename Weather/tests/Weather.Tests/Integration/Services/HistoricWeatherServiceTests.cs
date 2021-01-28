using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Moq;
using Weather.Core.Extensions;
using Weather.Core.Notifications;
using Weather.Domain.Repositories;
using Weather.Infra.Services;
using Xunit;

namespace Weather.Tests.Integration.Services
{
    public class HistoricWeatherServiceTests : TestIntegrationBase
    {
        private readonly IOptions<OpenWeatherSettings> _options;
        private readonly IMediatorHandler _mediatorHandler;
        private readonly IHistoricWeatherRepository _historicWeatherRepository;

        public HistoricWeatherServiceTests()
        {
            CreateScope();
            _options = Options.Create(new OpenWeatherSettings()
            {
                WeatherAppId = "a20bc11389c24a4d67419a201d95b798",
                WeatherBaseAddress = "http://api.openweathermap.org"
            });
            _historicWeatherRepository = GetInstance<IHistoricWeatherRepository>();
            _mediatorHandler = GetInstance<IMediatorHandler>();
        }

        [Fact]
        public async Task HistoricWeatherService_WhenGetByValidCityName_MustBeReturnWeather()
        {
            //Arrange
            var httpClient = new Mock<HttpClient>();
            var service = new HistoricWeatherService(httpClient.Object,
                _options,
                _mediatorHandler,
                _historicWeatherRepository);

            //Act
            var response = await service.GetHistoricByLatLon(-17.7311, -49.0994);

            //Assert
            Assert.NotNull(response);
            Assert.Equal(-17.7311, response?.Lat);
            Assert.Equal(-49.0994, response?.Lon);
        }
    }
}
