using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Weather.Domain.Models;

namespace Weather.Infra.Data.Mappings
{
    public class HistoricWeatherMapping : IEntityTypeConfiguration<HistoricWeather>
    {
        public void Configure(EntityTypeBuilder<HistoricWeather> builder)
        {
            builder.HasKey(c => c.Id);
            builder.ToTable("HistoricWeather");

            builder
                .HasMany(c => c.Daily)
                .WithOne(i => i.HistoricWeather)
                .HasForeignKey(c => c.HistoricWeatherId);
        }
    }
}
