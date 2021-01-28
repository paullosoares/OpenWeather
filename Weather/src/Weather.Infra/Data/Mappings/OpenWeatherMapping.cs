using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Weather.Domain.Models;

namespace Weather.Infra.Data.Mappings
{
    public class OpenWeatherMapping : IEntityTypeConfiguration<OpenWeather>
    {
        public void Configure(EntityTypeBuilder<OpenWeather> builder)
        {
            builder.HasKey(c => c.Id);
            builder.ToTable("OpenWeather");

            builder.OwnsOne(p => p.Coord, c =>
            {

                c.Property(pe => pe.Lat)
                    .HasColumnName("Latitude");

                c.Property(pe => pe.Lon)
                    .HasColumnName("Longitude");
            });

            builder.OwnsOne(p => p.Main, c =>
            {
                c.Property(pe => pe.FeelsLike)
                    .HasColumnName("FeelsLike");

                c.Property(pe => pe.Humidity)
                    .HasColumnName("Humidity");

                c.Property(pe => pe.Pressure)
                    .HasColumnName("Pressure");

                c.Property(pe => pe.Temp)
                    .HasColumnName("Temp");

                c.Property(pe => pe.TempMax)
                    .HasColumnName("TempMax");

                c.Property(pe => pe.TempMin)
                    .HasColumnName("TempMin");
            });
        }
    }
}
