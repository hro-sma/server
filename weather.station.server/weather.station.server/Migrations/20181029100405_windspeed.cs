using Microsoft.EntityFrameworkCore.Migrations;

namespace weather.station.server.Migrations
{
    public partial class windspeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "windspeed",
                table: "WeatherUpdate",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "windspeed",
                table: "WeatherUpdate");
        }
    }
}
