using FluentValidation;
using FluentValidation.Results;

namespace Weather.Domain.Models
{
    public class OpenWeatherCoordinate
    {
        public double Lon { get; private set; }
        public double Lat { get; private set; }

        //EF constructor
        protected OpenWeatherCoordinate() { }

        public OpenWeatherCoordinate(double lon, double lat)
        {
            Lon = lon;
            Lat = lat;
        }

        public ValidationResult IsValid()
        {
            return new OpenWeatherCoordinateValidation().Validate(this);
        }
    }

    public class OpenWeatherCoordinateValidation : AbstractValidator<OpenWeatherCoordinate>
    {
        public OpenWeatherCoordinateValidation()
        {
            RuleFor(c => c.Lon)
                .NotEqual(0)
                .WithMessage("Longitude is required.");

            RuleFor(c => c.Lat)
                .NotEqual(0)
                .WithMessage("Latitude is required.");
        }
    }
}
