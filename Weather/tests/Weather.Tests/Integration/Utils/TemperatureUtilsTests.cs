using System;
using Weather.Core.Utils;
using Xunit;

namespace Weather.Tests.Integration.Utils
{
    public class TemperatureUtilsTests
    {
        [Theory]
        [InlineData(308.65, 35.5)]
        [InlineData(314.05, 40.9)]
        [InlineData(310.55, 37.4)]
        public void TemperatureUtils_ConvertKelvinToCelsius_MustBeSuccess(double kelvin, double expected)
        { 
            //Arrange, Act, Assert
            Assert.Equal(expected, TemperatureUtils.ConvertKelvinToCelsius(kelvin));
        }

        [Theory]
        [InlineData(95.9, 35.5)]
        [InlineData(105.62, 40.9)]
        [InlineData(99.32, 37.4)]
        public void TemperatureUtils_ConvertToFahrenheitToCelsius_MustBeSuccess(double fahrenheit, double expected)
        {
            //Arrange, Act, Assert
            Assert.Equal(expected, TemperatureUtils.ConvertToFahrenheitToCelsius(fahrenheit));
        }

        [Theory]
        [InlineData(35.5, 95.9)]
        [InlineData(40.9, 105.62)]
        [InlineData(37.4, 99.32)]
        public void TemperatureUtils_ConvertCelsiusToFahrenheit_MustBeSuccess(double celsius, double expected)
        {
            //Arrange, Act, Assert
            Assert.Equal(expected, TemperatureUtils.ConvertCelsiusToFahrenheit(celsius));
        }
    }
}
