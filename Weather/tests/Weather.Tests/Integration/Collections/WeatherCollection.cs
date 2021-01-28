using System;
using Weather.Domain.Models;
using Xunit;

namespace Weather.Tests.Integration.Collections
{
    [CollectionDefinition(nameof(WeatherCollection))]
    public class WeatherCollection : ICollectionFixture<WeatherTestsFixture>
    {
    }

    public class WeatherTestsFixture : IDisposable
    {
        public OpenWeather GenerateValidWeather()
        {
            return new OpenWeather(
                "stations",
                visibility: 10000,
                dt: 1611759859,
                timezone: -10800,
                cod: 200,
                name: "Morrinhos");
        }

        public OpenWeather GenerateInValidWeather()
        {
            return new OpenWeather(
                "stations",
                visibility: 10000,
                dt: 1611759859,
                timezone: -10800,
                cod: 0,
                name: string.Empty);
        }

        public OpenWeatherCoordinate GenerateInValidCoordinate()
        {
            return new OpenWeatherCoordinate(0, 0);
        }

        public OpenWeatherCoordinate GenerateValidCoordinate()
        {
            return new OpenWeatherCoordinate(-49.0994, -17.7311);
        }

        public OpenWeatherMain GenerateInvalidOpenWeatherMain()
        {
            return new OpenWeatherMain(0, 0, 0, 0, 1013, 60);
        }

        public OpenWeatherMain GeneratevalidOpenWeatherMain()
        {
            return new OpenWeatherMain(301.28, 302.43, 0, 0, 1013, 60);
        }
        public void Dispose()
        {
        }
    }
}
