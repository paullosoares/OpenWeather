using Microsoft.Extensions.Options;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Weather.Core.Extensions;
using Weather.Core.Notifications;
using Weather.Domain.Dto;
using Weather.Domain.Models;
using Weather.Domain.Repositories;
using Weather.Domain.Services;

namespace Weather.Infra.Services
{
    public class OpenWeatherService : BaseService, IOpenWeatherService
    {
        private readonly HttpClient _httpClient;
        private readonly OpenWeatherSettings _appSettings;
        private readonly IWeatherRepository _weatherRepository;


        public OpenWeatherService(HttpClient httpClient,
                                IOptions<OpenWeatherSettings> options,
                                IWeatherRepository weatherRepository,
                                IMediatorHandler mediator) : base(mediator)
        {
            _appSettings = options.Value;
            httpClient.BaseAddress = new Uri(_appSettings.WeatherBaseAddress);
            _httpClient = httpClient;
            _weatherRepository = weatherRepository;
        }

        public async Task<OpenWeatherDto> GetTemperatureByCityName(string cityName)
        {
            var response = await _httpClient.GetAsync($"data/2.5/weather?q={cityName}&appid={_appSettings.WeatherAppId}&lang=pt_br");

            if (!TryResponseError(response))
                return null;

            var openWeatherDto = await DeserializeResponseObject<OpenWeatherDto>(response);

            if (openWeatherDto != null)
                await PersistOpenWeather(openWeatherDto);

            return openWeatherDto;
        }

        public async Task<OpenWeatherDto> GetTemperatureByLatLon(double latitude, double longitude)
        {
            var response = await _httpClient.GetAsync($"data/2.5/weather?lat={latitude}&lon={longitude}&appid={_appSettings.WeatherAppId}&lang=pt_br");

            if (!TryResponseError(response))
                return null;

            var openWeatherDto = await DeserializeResponseObject<OpenWeatherDto>(response);

            if (openWeatherDto != null)
                await PersistOpenWeather(openWeatherDto);

            return openWeatherDto;
        }

        public async Task PersistOpenWeather(OpenWeatherDto openWeatherDto)
        {
            await _weatherRepository.AddAsync(ConvertOpenWeatherDtoInOpenWeather(openWeatherDto));
            await _weatherRepository.UnitOfWork.Commit();
        }

        public OpenWeather ConvertOpenWeatherDtoInOpenWeather(OpenWeatherDto weatherDto)
        {
            OpenWeatherCoordinate coordinate = new OpenWeatherCoordinate(
                weatherDto.Coord.Lon,
                weatherDto.Coord.Lat);

            OpenWeatherMain weatherMain = new OpenWeatherMain(
                weatherDto.Main.Temp,
                weatherDto.Main.FeelsLike,
                weatherDto.Main.TempMin,
                weatherDto.Main.TempMax,
                weatherDto.Main.Pressure,
                weatherDto.Main.Humidity);

            OpenWeather openWeather = new OpenWeather(
                weatherDto.Base,
                weatherDto.Visibility,
                weatherDto.Dt,
                weatherDto.Timezone,
                weatherDto.Name,
                weatherDto.Cod);

            openWeather.AddCoordinate(coordinate);
            openWeather.AddWeatherMain(weatherMain);

            return openWeather;
        }
    }
}