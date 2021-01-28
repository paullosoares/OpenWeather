using System.Text.Json.Serialization;

namespace Weather.Domain.Dto
{
    public class OpenWeatherMainDto
    {
        public double Temp { get;  set; }
        [JsonPropertyName("feels_like")]
        public double FeelsLike { get;  set; }
        [JsonPropertyName("temp_min ")]
        public double TempMin { get;  set; }
        [JsonPropertyName("temp_max ")]
        public double TempMax { get;  set; }
        public double Pressure { get;  set; }
        public double Humidity { get;  set; }
    }
}
