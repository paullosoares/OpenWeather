using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Weather.Infra.Migrations
{
    public partial class CreateTableHistoricWeather : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "TempMin",
                table: "OpenWeather",
                nullable: true,
                oldClrType: typeof(float),
                oldType: "real",
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "TempMax",
                table: "OpenWeather",
                nullable: true,
                oldClrType: typeof(float),
                oldType: "real",
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "Temp",
                table: "OpenWeather",
                nullable: true,
                oldClrType: typeof(float),
                oldType: "real",
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "Pressure",
                table: "OpenWeather",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "Humidity",
                table: "OpenWeather",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "FeelsLike",
                table: "OpenWeather",
                nullable: true,
                oldClrType: typeof(float),
                oldType: "real",
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "Longitude",
                table: "OpenWeather",
                nullable: true,
                oldClrType: typeof(float),
                oldType: "real",
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "Latitude",
                table: "OpenWeather",
                nullable: true,
                oldClrType: typeof(float),
                oldType: "real",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "HistoricWeather",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Lat = table.Column<double>(nullable: false),
                    Lon = table.Column<double>(nullable: false),
                    Timezone = table.Column<string>(type: "varchar(100)", nullable: true),
                    TimezoneOffset = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistoricWeather", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HistoricWeatherDaily",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    HistoricWeatherId = table.Column<Guid>(nullable: false),
                    Dt = table.Column<int>(nullable: false),
                    Sunrise = table.Column<int>(nullable: false),
                    Sunset = table.Column<int>(nullable: false),
                    Day = table.Column<double>(nullable: true),
                    Temp_Min = table.Column<double>(nullable: true),
                    Max = table.Column<double>(nullable: true),
                    Night = table.Column<double>(nullable: true),
                    Eve = table.Column<double>(nullable: true),
                    Morn = table.Column<double>(nullable: true),
                    Pressure = table.Column<int>(nullable: false),
                    Humidity = table.Column<int>(nullable: false),
                    DewPoint = table.Column<double>(nullable: false),
                    WindSpeed = table.Column<double>(nullable: false),
                    WindDeg = table.Column<int>(nullable: false),
                    Clouds = table.Column<int>(nullable: false),
                    Pop = table.Column<double>(nullable: false),
                    Rain = table.Column<double>(nullable: false),
                    Uvi = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistoricWeatherDaily", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HistoricWeatherDaily_HistoricWeather_HistoricWeatherId",
                        column: x => x.HistoricWeatherId,
                        principalTable: "HistoricWeather",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HistoricWeatherDaily_HistoricWeatherId",
                table: "HistoricWeatherDaily",
                column: "HistoricWeatherId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HistoricWeatherDaily");

            migrationBuilder.DropTable(
                name: "HistoricWeather");

            migrationBuilder.AlterColumn<float>(
                name: "TempMin",
                table: "OpenWeather",
                type: "real",
                nullable: true,
                oldClrType: typeof(double),
                oldNullable: true);

            migrationBuilder.AlterColumn<float>(
                name: "TempMax",
                table: "OpenWeather",
                type: "real",
                nullable: true,
                oldClrType: typeof(double),
                oldNullable: true);

            migrationBuilder.AlterColumn<float>(
                name: "Temp",
                table: "OpenWeather",
                type: "real",
                nullable: true,
                oldClrType: typeof(double),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Pressure",
                table: "OpenWeather",
                type: "int",
                nullable: true,
                oldClrType: typeof(double),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Humidity",
                table: "OpenWeather",
                type: "int",
                nullable: true,
                oldClrType: typeof(double),
                oldNullable: true);

            migrationBuilder.AlterColumn<float>(
                name: "FeelsLike",
                table: "OpenWeather",
                type: "real",
                nullable: true,
                oldClrType: typeof(double),
                oldNullable: true);

            migrationBuilder.AlterColumn<float>(
                name: "Longitude",
                table: "OpenWeather",
                type: "real",
                nullable: true,
                oldClrType: typeof(double),
                oldNullable: true);

            migrationBuilder.AlterColumn<float>(
                name: "Latitude",
                table: "OpenWeather",
                type: "real",
                nullable: true,
                oldClrType: typeof(double),
                oldNullable: true);
        }
    }
}
