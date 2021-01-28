using System;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using Weather.Core.Data;
using Weather.Core.Notifications;
using Weather.Domain.Models;

namespace Weather.Infra.Data
{
    public sealed class OpenWeatherContext : DbContext, IUnitOfWork
    {
        private readonly IMediatorHandler _mediator;
        public OpenWeatherContext(DbContextOptions<OpenWeatherContext> options, IMediatorHandler mediator) : base(options)
        {
            _mediator = mediator;
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            ChangeTracker.AutoDetectChangesEnabled = false;
        }

        public DbSet<OpenWeather> OpenWeather { get; set; }
        public DbSet<HistoricWeather> HistoricWeather { get; set; }
        public DbSet<HistoricWeatherDaily> HistoricWeatherDaily { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // STRINGS TO VARCHAR(100)
            foreach (var property in modelBuilder.Model.GetEntityTypes().SelectMany(
                e => e.GetProperties().Where(p => p.ClrType == typeof(string))))
                property.SetColumnType("varchar(100)");

            // IGNORE FLUENTVALIDATION
            modelBuilder.Ignore<ValidationResult>();

            // MAPPINGS
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(OpenWeatherContext).Assembly);


            //CASCADE DELETE
            foreach (var relationship in modelBuilder.Model.GetEntityTypes()
                .SelectMany(e => e.GetForeignKeys())) relationship.DeleteBehavior = DeleteBehavior.Cascade;


            base.OnModelCreating(modelBuilder);
        }

        public async Task<bool> Commit()
        {
            var success = await base.SaveChangesAsync() > 0;

            if (!success)
            {
                await _mediator.SendEvent(new Notification(String.Empty, "Não foi possível persistir os dados no banco"));
            }
            return success;
        }
    }
}
