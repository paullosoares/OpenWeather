using System.Collections.Generic;
using System.Text.Json.Serialization;
using Weather.Core.DomainObjects;

namespace Weather.Domain.Dto
{
    public class HistoricWeatherDto  
    {
        public double Lat { get; set; }
        public double Lon { get; set; }
        public string Timezone { get; set; }
        [JsonPropertyName("timezone_offset ")]
        public int TimezoneOffset { get; set; }
        public List<HistoricWeatherDailyDto> Daily { get; set; }
    }
}
