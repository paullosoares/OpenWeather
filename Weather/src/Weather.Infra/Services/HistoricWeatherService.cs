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
    public class HistoricWeatherService : BaseService, IHistoricWeatherService
    {
        private readonly HttpClient _httpClient;
        private readonly OpenWeatherSettings _appSettings;
        private readonly IHistoricWeatherRepository _historicWeatherRepository;
        public HistoricWeatherService(HttpClient httpClient,
                                        IOptions<OpenWeatherSettings> options,
                                        IMediatorHandler mediator, 
                                        IHistoricWeatherRepository historicWeatherRepository) : base(mediator)
        {
            _appSettings = options.Value;
            httpClient.BaseAddress = new Uri(_appSettings.WeatherBaseAddress);
            _httpClient = httpClient;
            _historicWeatherRepository = historicWeatherRepository;
        }

        public async Task<HistoricWeatherDto> GetHistoricByLatLon(double latitude, double longitude)
        {
            var requestUri = $"data/2.5/onecall?lat={latitude}&lon={longitude}&appid={_appSettings.WeatherAppId}";
            var response = await _httpClient.GetAsync(requestUri);
            if (!TryResponseError(response))
                return null;

            
            var historicWeatherDto = await DeserializeResponseObject<HistoricWeatherDto>(response);

            if (historicWeatherDto != null)
                await PersistHistoricWeather(historicWeatherDto);

            return historicWeatherDto;
        }
        
        public async Task PersistHistoricWeather(HistoricWeatherDto openWeatherDto)
        {
            await _historicWeatherRepository.AddAsync(ConvertHistoricWeatherDtoInHistoricWeather(openWeatherDto));
            await _historicWeatherRepository.UnitOfWork.Commit();
        }

        public HistoricWeather ConvertHistoricWeatherDtoInHistoricWeather(HistoricWeatherDto dto)
        {
            HistoricWeather historicWeather = new HistoricWeather(
                dto.Lat, 
                dto.Lon, 
                dto.Timezone, 
                dto.TimezoneOffset);

            foreach (var daily in dto.Daily)
            {
                HistoricWeatherDaily historicWeatherDaily = new HistoricWeatherDaily(
                    historicWeather.Id,
                    daily.Dt,
                    daily.Sunrise,
                    daily.Sunset,
                    daily.Pressure,
                    daily.Humidity,
                    daily.DewPoint,
                    daily.WindSpeed,
                    daily.WindDeg,
                    daily.Clouds,
                    daily.Pop,
                    daily.Rain,
                    daily.Uvi);

                historicWeatherDaily.AddHistoricTemp(new HistoricTemp
                {
                    Day = daily.Temp.Day,
                    Min = daily.Temp.Min,
                    Max = daily.Temp.Max,
                    Night = daily.Temp.Night,
                    Eve = daily.Temp.Eve,
                    Morn = daily.Temp.Morn
                });

                historicWeather.AddHistoricWeatherDaily(historicWeatherDaily);
            }

            return historicWeather;
        }
    }
}
