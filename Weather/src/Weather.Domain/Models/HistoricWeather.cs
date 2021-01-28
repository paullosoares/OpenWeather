using System;
using System.Collections.Generic;
using FluentValidation;
using Weather.Core.DomainObjects;

namespace Weather.Domain.Models
{
    public class HistoricWeather : Entity, IAggregateRoot
    {
        public double Lat { get; private set; }
        public double Lon { get; private set; }
        public string Timezone { get; private set; }
        public int TimezoneOffset { get; private set; }
        public List<HistoricWeatherDaily> Daily { get; private set; }

        public HistoricWeather() { }

        public HistoricWeather(double lat, double lon, string timezone, int timezoneOffset)
        {
            Lat = lat;
            Lon = lon;
            Timezone = timezone;
            TimezoneOffset = timezoneOffset;
        }

        public void AddHistoricWeatherDaily(HistoricWeatherDaily daily)
        {
            Daily ??= new List<HistoricWeatherDaily>();

            Daily.Add(daily);
        }

        public override bool Isvalid()
        {
            ValidationResult = new HistoricWeatherValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class HistoricWeatherValidation : AbstractValidator<HistoricWeather>
    {
        public HistoricWeatherValidation()
        {
            RuleFor(c => c.Id)
                .NotEqual(Guid.Empty)
                .WithMessage("Id is required.");

            RuleFor(c => c.Lat)
                .NotEmpty()
                .WithMessage("Lat is required.");

            RuleFor(c => c.Lon)
                .NotEqual(0)
                .WithMessage("Lon is required.");
        }
    }
}
