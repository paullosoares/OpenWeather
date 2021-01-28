using System;
using FluentValidation;
using FluentValidation.Results;
using Weather.Core.DomainObjects;

namespace Weather.Domain.Models
{
    public class OpenWeather : Entity, IAggregateRoot
    {
        public OpenWeatherCoordinate Coord { get; private set; }
        public string Base { get; private set; }
        public OpenWeatherMain Main { get; private set; }
        public int Visibility { get; private set; }
        public int Dt { get; private set; }
        public int Timezone { get; private set; }
        public string Name { get; private set; }
        public int Cod { get; private set; }

        //EF constructor
        protected OpenWeather() { }

        public OpenWeather(string @base, int visibility, int dt, int timezone, string name, int cod)
        {
            Base = @base;
            Visibility = visibility;
            Dt = dt;
            Timezone = timezone;
            Name = name;
            Cod = cod;
        }

        public ValidationResult AddCoordinate(OpenWeatherCoordinate coord)
        {
            var result = coord.IsValid();
            if (!result.IsValid) return result;

            Coord = coord;
            return result;
        }

        public ValidationResult AddWeatherMain(OpenWeatherMain main)
        {
            var result = main.IsValid();
            if (!result.IsValid) return result;

            Main = main;
            return result;
        }

        public override bool Isvalid()
        {
            ValidationResult = new OpenWeatherValidation().Validate(this);
            return ValidationResult.IsValid;
        }

        public void UpdateName(string newName)
        {
            Name = newName;
        }
    }

    public class OpenWeatherValidation : AbstractValidator<OpenWeather>
    {
        public OpenWeatherValidation()
        {
            RuleFor(c => c.Id)
                .NotEqual(Guid.Empty)
                .WithMessage("Id is required.");

            RuleFor(c => c.Name)
                .NotEmpty()
                .WithMessage("Name is required.");

            RuleFor(c => c.Cod)
                .NotEqual(0)
                .WithMessage("Cod is required.");
        }
    }
}
