using Microsoft.EntityFrameworkCore.Migrations;

namespace weather.station.server.Migrations
{
    public partial class windspeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Windspeed",
                table: "WeatherUpdate",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Windspeed",
                table: "WeatherUpdate");
        }
    }
}
