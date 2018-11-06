using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace weather.station.server.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WeatherUpdate",
                columns: table => new
                {
                    WeatherUpdateId = table.Column<Guid>(nullable: false),
                    TimeStamp = table.Column<DateTime>(nullable: false),
                    TemperatureC = table.Column<double>(nullable: false),
                    Humidity = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeatherUpdate", x => x.WeatherUpdateId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WeatherUpdate");
        }
    }
}
