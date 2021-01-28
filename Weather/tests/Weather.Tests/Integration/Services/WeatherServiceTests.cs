using MediatR;
using Microsoft.Extensions.Options;
using Moq;
using System.Net.Http;
using System.Threading.Tasks;
using Weather.Core.Extensions;
using Weather.Core.Notifications;
using Weather.Domain.Repositories;
using Weather.Infra.Services;
using Xunit;

namespace Weather.Tests.Integration.Services
{
    public class WeatherServiceTests : TestIntegrationBase
    {
        private readonly IOptions<OpenWeatherSettings> _options;
        private readonly IMediatorHandler _mediatorHandler;
        private readonly IWeatherRepository _weatherRepository;
        private readonly NotificationHandler _notifications;

        public WeatherServiceTests()
        {
            CreateScope();
            _options = Options.Create(new OpenWeatherSettings()
            {
                WeatherAppId = "a20bc11389c24a4d67419a201d95b798",
                WeatherBaseAddress = "http://api.openweathermap.org"
            });
            _weatherRepository = GetInstance<IWeatherRepository>();
            _mediatorHandler = GetInstance<IMediatorHandler>();
            _notifications = (NotificationHandler)GetInstance<INotificationHandler<Notification>>();

        }

        [Fact]
        public async Task WeatherService_WhenGetByValidCityName_MustBeReturnWeather()
        {
            //Arrange
            var httpClient = new Mock<HttpClient>();
            var service = new OpenWeatherService(httpClient.Object,
                                                _options,
                                                _weatherRepository,
                                                _mediatorHandler);

            //Act
            var response = await service.GetTemperatureByCityName(cityName: "Morrinhos");

            //Assert
            Assert.NotNull(response);
            Assert.Equal("Morrinhos", response?.Name);
        }

        [Fact]
        public async Task WeatherService_WhenGetByInValidCityName_MustBeReturnNotification()
        {
            //Arrange
            var httpClient = new Mock<HttpClient>();
            var service = new OpenWeatherService(httpClient.Object,
                _options,
                _weatherRepository,
                _mediatorHandler);

            //Act
            var response = await service.GetTemperatureByCityName(cityName: "InvalidCityName");

            //Act, Assert
              Assert.Null(response);
              Assert.True(_notifications.HasNotifications());
              Assert.Single(_notifications.GetNotifications());
        }

        [Fact]
        public async Task WeatherService_WhenGetByByLatLon_MustBeReturnWeather()
        {
            //Arrange
            var httpClient = new Mock<HttpClient>();
            var service = new OpenWeatherService(httpClient.Object,
                _options,
                _weatherRepository,
                _mediatorHandler);

            //Act
            var response = await service.GetTemperatureByLatLon(-17.7311, -49.0994);

            //Assert
            Assert.NotNull(response);
            Assert.Equal("Morrinhos", response?.Name);
        }
    }
}
