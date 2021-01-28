using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using Weather.Core.Data;
using Weather.Core.Notifications;
using Weather.Domain.Repositories;
using Weather.Domain.Services;
using Weather.Infra.Data;
using Weather.Infra.Data.Repositories;
using Weather.Infra.Services;
using Weather.Tests.Integration.Fake;

namespace Weather.Tests.Integration
{
    public class TestIntegrationBase : IDisposable
    {
        private IServiceCollection _service;
        private OpenWeatherContext _context;
        private IServiceScope _serviceScope;
        private readonly NotificationHandler _notificationHandler = new NotificationHandler();

        public TestIntegrationBase()
        {
            _service = new ServiceCollection();
            RegisterDependencyInjection(_service);
            ConfigureDbContext();
        }

        private void ConfigureDbContext()
        {
            var options = new DbContextOptionsBuilder<OpenWeatherContext>()
                .UseInMemoryDatabase("weather_db")
                .Options;

            _context = new OpenWeatherContext(options, GetInstance<IMediatorHandler>());
            _context.Database.EnsureCreated();
        }

        public void Dispose()
        {
            _context?.Dispose();
            _serviceScope?.Dispose();
        }


        private void RegisterDependencyInjection(IServiceCollection services)
        {
            services.AddScoped(provider => _context);

            //Mediator
            services.AddMediatR(AppDomain.CurrentDomain.Load("Weather.Tests"));

            //Repositories
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IWeatherRepository, WeatherRepository>();
            services.AddScoped<IHistoricWeatherRepository, HistoricWeatherRepository>();
            
            //Notifications
            services.AddScoped<IMediatorHandler, MediatorHandler>();
            services.AddScoped<INotificationHandler<Notification>, NotificationHandler>(provider => _notificationHandler);
        }

        protected void CreateScope()
        {
            var buildServiceProvider = _service.BuildServiceProvider();
            _serviceScope = buildServiceProvider.CreateScope();
        }

        protected T GetInstanceScope<T>()
        {
            var scopeProvider = _serviceScope?.ServiceProvider;
            return _serviceScope == null ? default(T) : scopeProvider.GetService<T>();
        }

        protected T GetInstance<T>()
        {
            var scopeProvider = _service.BuildServiceProvider();
            return scopeProvider.GetService<T>();
        }

    }
}
