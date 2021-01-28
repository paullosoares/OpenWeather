using System;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Weather.Core.Data;
using Weather.Core.Notifications;
using Weather.Domain.Repositories;
using Weather.Domain.Services;
using Weather.Infra.Data;
using Weather.Infra.Data.Repositories;
using Weather.Infra.Services;

namespace Weather.Api.Configuration
{
    public static class DependencyInjectionConfiguration
    {
        public static void AddDependencyInjectionConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<OpenWeatherContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });

            //REPOSITORIES
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IWeatherRepository, WeatherRepository>();
            services.AddScoped<IHistoricWeatherRepository, HistoricWeatherRepository>();

            //SERVICES
            services.AddHttpClient<IOpenWeatherService, OpenWeatherService>();
            services.AddHttpClient<IHistoricWeatherService, HistoricWeatherService>();

            //NOTIFICATIONS
            services.AddScoped<IMediatorHandler, MediatorHandler>();
            services.AddScoped<INotificationHandler<Notification>, NotificationHandler>();
            services.AddMediatR(AppDomain.CurrentDomain.Load("Weather.Core"));
        }
    }
}
