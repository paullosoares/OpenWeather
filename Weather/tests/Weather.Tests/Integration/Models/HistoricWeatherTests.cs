using Weather.Tests.Integration.Collections;
using Xunit;

namespace Weather.Tests.Integration.Models
{
    [Collection(nameof(HistoricWeatherCollection))]
    public class HistoricWeatherTests
    {
        private readonly HistoricWeatherTestsFixture _historicWeatherTestsFixture;

        public HistoricWeatherTests(HistoricWeatherTestsFixture historicWeatherTestsFixture)
        {
            _historicWeatherTestsFixture = historicWeatherTestsFixture;
        }

        [Fact(DisplayName = "Add historic weather must be valid")]
        [Trait("Categoria", "HistoricWeather Tests")]
        public void HistoricWeather_NewHistoricWeather_MustBeValid()
        {
            //Arrange
            var historicWeather = _historicWeatherTestsFixture.GenerateValidHistoricWeather();

            //Act
            var result = historicWeather.Isvalid();

            //Assert
            Assert.True(result);
            Assert.Equal(0, historicWeather.ValidationResult.Errors.Count);
        }

        [Fact(DisplayName = "Add historic weather must be invalid")]
        [Trait("Categoria", "HistoricWeather Tests")]
        public void HistoricWeather_NewHistoricWeather_MustBeInValid()
        {
            //Arrange
            var historicWeather = _historicWeatherTestsFixture.GenerateInValidHistoricWeather();

            //Act
            var result = historicWeather.Isvalid();

            //Assert
            Assert.False(result);
            Assert.NotEqual(0, historicWeather.ValidationResult.Errors.Count);
        }

        [Fact(DisplayName = "Add historic weather daily must be valid")]
        [Trait("Categoria", "HistoricWeather Tests")]
        public void HistoricWeather_AddHistoricWeatherDaily_DailyMustBeValid()
        {
            //Arrange
            var historicWeatherDaily = _historicWeatherTestsFixture.GenerateValidHistoricWeatherDaily();

            //Act
            var validationResult = historicWeatherDaily.Isvalid();

            //Assert
            Assert.True(validationResult.IsValid);
            Assert.Equal(0, validationResult.Errors.Count);
        }

        [Fact(DisplayName = "Add historic weather daily must be invalid")]
        [Trait("Categoria", "HistoricWeather Tests")]
        public void HistoricWeather_AddHistoricWeatherDaily_DailyMustBeInValid()
        {
            //Arrange
            var historicWeatherDaily = _historicWeatherTestsFixture.GenerateInValidHistoricWeatherDaily();

            //Act
            var validationResult = historicWeatherDaily.Isvalid();

            //Assert
            Assert.False(validationResult.IsValid);
            Assert.NotEqual(0, validationResult.Errors.Count);
        }
    }
}
