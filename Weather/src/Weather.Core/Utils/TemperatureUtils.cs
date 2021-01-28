using System;

namespace Weather.Core.Utils
{
    public static class TemperatureUtils
    {
        public static double ConvertKelvinToCelsius(double kelvin)
        {
            return Math.Round(kelvin - 273.15, 2);
        }

        public static double ConvertToFahrenheitToCelsius(double fahrenheit)
        {
            return Math.Round((fahrenheit - 32) * 5 / 9, 2);
        }

        public static double ConvertCelsiusToFahrenheit(double celsius)
        {
            return Math.Round((celsius * 9) / 5 + 32, 2);
        }
    }
}
