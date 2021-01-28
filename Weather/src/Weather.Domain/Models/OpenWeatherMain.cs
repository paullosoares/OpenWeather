using FluentValidation;
using FluentValidation.Results;

namespace Weather.Domain.Models
{
    public class OpenWeatherMain
    {
        public double Temp { get; private set; }
        public double FeelsLike { get; private set; }
        public double TempMin { get; private set; }
        public double TempMax { get; private set; }
        public double Pressure { get; private set; }
        public double Humidity { get; private set; }

        //EF constructor
        protected OpenWeatherMain(){ }

        public OpenWeatherMain(double temp, double feelsLike, double tempMin, double tempMax, double pressure, double humidity)
        {
            Temp = temp;
            FeelsLike = feelsLike;
            TempMin = tempMin;
            TempMax = tempMax;
            Pressure = pressure;
            Humidity = humidity;
        }

        public ValidationResult IsValid()
        {
            return new OpenWeatherMainValidation().Validate(this);
        }
    }

    public class OpenWeatherMainValidation : AbstractValidator<OpenWeatherMain>
    {
        public OpenWeatherMainValidation()
        {
            RuleFor(c => c.Temp)
                .NotEmpty()
                .WithMessage("Temp is required.");

            RuleFor(c => c.FeelsLike)
                .NotEqual(0)
                .WithMessage("FeelsLike is required.");
        }
    }
}
