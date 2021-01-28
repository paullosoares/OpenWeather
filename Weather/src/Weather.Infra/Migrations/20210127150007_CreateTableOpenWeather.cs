using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Weather.Infra.Migrations
{
    public partial class CreateTableOpenWeather : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OpenWeather",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Longitude = table.Column<float>(nullable: true),
                    Latitude = table.Column<float>(nullable: true),
                    Base = table.Column<string>(type: "varchar(100)", nullable: true),
                    Temp = table.Column<float>(nullable: true),
                    FeelsLike = table.Column<float>(nullable: true),
                    TempMin = table.Column<float>(nullable: true),
                    TempMax = table.Column<float>(nullable: true),
                    Pressure = table.Column<int>(nullable: true),
                    Humidity = table.Column<int>(nullable: true),
                    Visibility = table.Column<int>(nullable: false),
                    Dt = table.Column<int>(nullable: false),
                    Timezone = table.Column<int>(nullable: false),
                    Name = table.Column<string>(type: "varchar(100)", nullable: true),
                    Cod = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OpenWeather", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OpenWeather");
        }
    }
}
