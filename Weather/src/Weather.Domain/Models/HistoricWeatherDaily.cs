using System;
using System.Text.Json.Serialization;
using FluentValidation;
using FluentValidation.Results;
using Weather.Core.DomainObjects;

namespace Weather.Domain.Models
{
    public class HistoricWeatherDaily : Entity
    {
        public Guid HistoricWeatherId { get; private set; }
        public int Dt { get; private set; }
        public int Sunrise { get; private set; }
        public int Sunset { get; private set; }
        public HistoricTemp Temp { get; private set; }
        public int Pressure { get; private set; }
        public int Humidity { get; private set; }
        public double DewPoint { get; private set; }
        public double WindSpeed { get; private set; }
        public int WindDeg { get; private set; }
        public int Clouds { get; private set; }
        public double Pop { get; private set; }
        public double Rain { get; private set; }
        public double Uvi { get; private set; }

        [JsonIgnore]
        public HistoricWeather HistoricWeather { get; set; }

        protected HistoricWeatherDaily() { }

        public HistoricWeatherDaily(Guid historicWeatherId, int dt, int sunrise, int sunset, int pressure, int humidity, double dewPoint, double windSpeed, int windDeg, int clouds, double pop, double rain, double uvi)
        {
            HistoricWeatherId = historicWeatherId;
            Dt = dt;
            Sunrise = sunrise;
            Sunset = sunset;
            Pressure = pressure;
            Humidity = humidity;
            DewPoint = dewPoint;
            WindSpeed = windSpeed;
            WindDeg = windDeg;
            Clouds = clouds;
            Pop = pop;
            Rain = rain;
            Uvi = uvi;
        }

        public void AddHistoricTemp(HistoricTemp historicTemp)
        {
            Temp = historicTemp;
        }

        public ValidationResult Isvalid()
        {
            return new HistoricWeatherDailyValidation().Validate(this);
        }
    }

    public class HistoricWeatherDailyValidation : AbstractValidator<HistoricWeatherDaily>
    {
        public HistoricWeatherDailyValidation()
        {
            RuleFor(c => c.HistoricWeatherId)
                .NotEqual(Guid.Empty)
                .WithMessage("HistoricWeatherId is required.");

            RuleFor(c => c.Dt)
                .NotEqual(0)
                .WithMessage("Dt is required.");
        }
    }
}
