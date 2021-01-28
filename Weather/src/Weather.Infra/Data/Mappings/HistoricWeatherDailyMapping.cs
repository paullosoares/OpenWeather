using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using Weather.Domain.Models;

namespace Weather.Infra.Data.Mappings
{
    public class HistoricWeatherDailyMapping : IEntityTypeConfiguration<HistoricWeatherDaily>
    {
        public void Configure(EntityTypeBuilder<HistoricWeatherDaily> builder)
        {
            builder.HasKey(c => c.Id);
            builder.ToTable("HistoricWeatherDaily");

            builder.OwnsOne(p => p.Temp, c =>
            {
                c.Property(pe => pe.Day)
                    .HasColumnName("Day");

                c.Property(pe => pe.Max)
                    .HasColumnName("Max");

                c.Property(pe => pe.Night)
                    .HasColumnName("Night");

                c.Property(pe => pe.Eve)
                    .HasColumnName("Eve");

                c.Property(pe => pe.Morn)
                    .HasColumnName("Morn");
            });
        }
    }
}
