namespace Weather.Domain.Dto
{
    public class OpenWeatherDto  
    {
        public OpenWeatherCoordinateDto Coord { get; set; }
        public string Base { get; set; }
        public  OpenWeatherMainDto Main { get; set; }
        public int Visibility { get; set; }
        public int Dt { get; set; }
        public int Timezone { get; set; }
        public string Name { get; set; }
        public int Cod { get; set; }
    }
}
