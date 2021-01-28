using System;
using Weather.Domain.Models;
using Xunit;

namespace Weather.Tests.Integration.Collections
{
    [CollectionDefinition(nameof(HistoricWeatherCollection))]
    public class HistoricWeatherCollection : ICollectionFixture<HistoricWeatherTestsFixture>
    {
    }

    public class HistoricWeatherTestsFixture : IDisposable
    {

        public HistoricWeather GenerateValidHistoricWeather()
        {
            HistoricWeather weather = new HistoricWeather(-17.7311, -49.0994, "America/Sao_Paulo", 0);
            
            for (int i = 0; i < 10; i++)
            {
                weather.AddHistoricWeatherDaily(GenerateValidHistoricWeatherDaily());
            }

            return weather;
        }

        public HistoricWeather GenerateInValidHistoricWeather()
        {
            HistoricWeather weather = new HistoricWeather(0, 0, string.Empty, 0);

            for (int i = 0; i < 10; i++)
            {
                weather.AddHistoricWeatherDaily(GenerateInValidHistoricWeatherDaily());
            }

            return weather;
        }

        public HistoricWeatherDaily GenerateInValidHistoricWeatherDaily()
        {
            return new HistoricWeatherDaily(
                Guid.Empty,
                0, 0, 0, 0, 0, 0,
                0, 0, 0, 0.0, 0.0, 0.0);
        }

        public HistoricWeatherDaily GenerateValidHistoricWeatherDaily()
        {
            return new HistoricWeatherDaily(
                Guid.NewGuid(),
                new Random().Next(), 0, 0, 0, 0, 0,
                0, 0, 0, 0.0, 0.0, 0.0);
        }


        public void Dispose()
        {
        }
    }
}
