using Moq;
using System.Threading.Tasks;
using Weather.Domain.Repositories;
using Weather.Tests.Integration.Collections;
using Xunit;

namespace Weather.Tests.Integration.Repositories
{
    [Collection(nameof(WeatherCollection))]
    public class WeatherRepositoryTests
    {
        private readonly WeatherTestsFixture _weatherTestsFixture;
        public WeatherRepositoryTests(WeatherTestsFixture weatherTestsFixture)
        {
            _weatherTestsFixture = weatherTestsFixture;
        }

        [Fact(DisplayName = "Weather add is Success")]
        [Trait("Categoria", "Weather Repository Tests")]
        public async Task WeatherRepository_Add_ReturnSuccess()
        {
            // Arrange
            var weatherRepository = new Mock<IWeatherRepository>();
            var weather = _weatherTestsFixture.GenerateValidWeather();

            // Act
            await weatherRepository.Object.AddAsync(weather);

            // Assert
            Assert.True(weather.Isvalid());
            weatherRepository.Verify(r => r.AddAsync(weather), Times.Once);
        }
    }
}
