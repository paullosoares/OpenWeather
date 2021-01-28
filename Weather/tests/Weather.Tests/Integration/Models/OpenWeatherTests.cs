using Weather.Domain.Models;
using Weather.Tests.Integration.Collections;
using Xunit;

namespace Weather.Tests.Integration.Models
{
    [Collection(nameof(WeatherCollection))]
    public class OpenWeatherTests
    {
        private readonly WeatherTestsFixture _weatherTestsFixture;

        public OpenWeatherTests(WeatherTestsFixture weatherTestsFixture)
        {
            _weatherTestsFixture = weatherTestsFixture;
        }
        
        [Fact(DisplayName = "New OpenWeather valid")]
        [Trait("Categoria", "OpenWeather Tests")]
        public void OpenWeather_NewOpenWeather_IsValid()
        {
            //Arrange
            var openWeather = _weatherTestsFixture.GenerateValidWeather();
            var coordinate = _weatherTestsFixture.GenerateValidCoordinate();
            var weatherMain = _weatherTestsFixture.GeneratevalidOpenWeatherMain();

            //Act
            var openWeatherResult = openWeather.Isvalid();
            var addCoordinateResult = openWeather.AddCoordinate(coordinate);
            var addWeatherMainResult = openWeather.AddWeatherMain(weatherMain);

            //Assert
            Assert.True(openWeatherResult);
            Assert.True(addCoordinateResult.IsValid);
            Assert.True(addWeatherMainResult.IsValid);
            Assert.Equal(0, openWeather.ValidationResult.Errors.Count);
            Assert.Equal(0, addCoordinateResult.Errors.Count);
            Assert.Equal(0, addWeatherMainResult.Errors.Count);
        }

        [Fact(DisplayName = "New OpenWeather invalid")]
        [Trait("Categoria", "OpenWeather Tests")]
        public void OpenWeather_NewOpenWeather_InValid()
        {
            //Arrange
            var openWeather = _weatherTestsFixture.GenerateInValidWeather();

            //Act

            var result = openWeather.Isvalid();

            //Assert
            Assert.False(result);
            Assert.NotEqual(0, openWeather.ValidationResult.Errors.Count);
        }


        [Fact(DisplayName = "New OpenWeather valid coordinate")]
        [Trait("Categoria", "OpenWeather Tests")]
        public void OpenWeather_NewOpenWeather_IsValidCoordinate()
        {
            //Arrange
            var openWeather = _weatherTestsFixture.GenerateValidWeather();
            var addCoordinateResult = openWeather.AddCoordinate(_weatherTestsFixture.GenerateValidCoordinate());

            //Act

            var result = addCoordinateResult.IsValid;

            //Assert
            Assert.True(result);
            Assert.Equal(0, addCoordinateResult.Errors.Count);
        }

        [Fact(DisplayName = "New OpenWeather invalid coordinate")]
        [Trait("Categoria", "OpenWeather Tests")]
        public void OpenWeather_NewOpenWeather_InValidCoordinate()
        {
            //Arrange
            var openWeather = _weatherTestsFixture.GenerateInValidWeather();
            var addCoordinateResult = openWeather.AddCoordinate(_weatherTestsFixture.GenerateInValidCoordinate());

            //Act
            var result = addCoordinateResult.IsValid;

            //Assert
            Assert.False(result);
            Assert.NotEqual(0, addCoordinateResult.Errors.Count);
        }

        [Fact(DisplayName = "New OpenWeather valid weather main")]
        [Trait("Categoria", "OpenWeather Tests")]
        public void OpenWeather_NewOpenWeather_IsValidWeatherMain()
        {
            //Arrange
            var openWeather = _weatherTestsFixture.GenerateValidWeather();
            var addWeatherMain = openWeather.AddWeatherMain(_weatherTestsFixture.GeneratevalidOpenWeatherMain());

            //Act

            var result = addWeatherMain.IsValid;

            //Assert
            Assert.True(result);
            Assert.Equal(0, addWeatherMain.Errors.Count);
        }

        [Fact(DisplayName = "New OpenWeather invalid weather main")]
        [Trait("Categoria", "OpenWeather Tests")]
        public void OpenWeather_NewOpenWeather_InValidWeatherMain()
        {
            //Arrange
            var openWeather = _weatherTestsFixture.GenerateValidWeather();
            var addWeatherMain = openWeather.AddWeatherMain(_weatherTestsFixture.GenerateInvalidOpenWeatherMain());

            //Act
            var result = addWeatherMain.IsValid;

            //Assert
            Assert.False(result);
            Assert.NotEqual(0, addWeatherMain.Errors.Count);
        }
    }
}
