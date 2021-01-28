using System.Threading.Tasks;
using Moq;
using Weather.Domain.Repositories;
using Weather.Tests.Integration.Collections;
using Xunit;

namespace Weather.Tests.Integration.Repositories
{
    [Collection(nameof(HistoricWeatherCollection))]
    public class HistoricWeatherRepositoryTests
    {
        private readonly HistoricWeatherTestsFixture _historicWeatherTestsFixture;

        public HistoricWeatherRepositoryTests(HistoricWeatherTestsFixture historicWeatherTestsFixture)
        {
            _historicWeatherTestsFixture = historicWeatherTestsFixture;
        }

        [Fact(DisplayName = "Historic Weather add is Success")]
        [Trait("Categoria", "Historic Weather Repository Tests")]
        public async Task WeatherRepository_Add_ReturnSuccess()
        {
            // Arrange
            var historicWeatherRepository = new Mock<IHistoricWeatherRepository>();
            var historicWeather = _historicWeatherTestsFixture.GenerateValidHistoricWeather();

            // Act
            await historicWeatherRepository.Object.AddAsync(historicWeather);

            // Assert
            Assert.True(historicWeather.Isvalid());
            historicWeatherRepository.Verify(r => r.AddAsync(historicWeather), Times.Once);
        }
    }
}
