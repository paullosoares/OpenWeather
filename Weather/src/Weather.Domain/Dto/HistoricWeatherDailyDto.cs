using System.Text.Json.Serialization;

namespace Weather.Domain.Dto
{
    public class HistoricWeatherDailyDto
    {
        public int Dt { get; set; }
        public int Sunrise { get; set; }
        public int Sunset { get; set; }
        public HistoricTempDto Temp { get; set; }
        public int Pressure { get; set; }
        public int Humidity { get; set; }
        [JsonPropertyName("dew_point")]
        public double DewPoint { get; set; }
        public double WindSpeed { get; set; }
        [JsonPropertyName("wind_deg")]
        public int WindDeg { get; set; }
        public int Clouds { get; set; }
        public double Pop { get; set; }
        public double Rain { get; set; }
        public double Uvi { get; set; }
    }
}
