using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace weather.station.server.Migrations
{
    public partial class final : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Device",
                columns: table => new
                {
                    DeviceId = table.Column<Guid>(nullable: false),
                    StudentNumber = table.Column<string>(nullable: true),
                    DeviceName = table.Column<string>(nullable: true),
                    Longitude = table.Column<double>(nullable: false),
                    Latitude = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Device", x => x.DeviceId);
                });

            migrationBuilder.CreateTable(
                name: "WeatherUpdate",
                columns: table => new
                {
                    WeatherUpdateId = table.Column<Guid>(nullable: false),
                    DeviceId = table.Column<Guid>(nullable: false),
                    TimeStamp = table.Column<DateTime>(nullable: false),
                    TemperatureC = table.Column<double>(nullable: false),
                    Humidity = table.Column<double>(nullable: false),
                    Windspeed = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeatherUpdate", x => x.WeatherUpdateId);
                    table.ForeignKey(
                        name: "FK_WeatherUpdate_Device_DeviceId",
                        column: x => x.DeviceId,
                        principalTable: "Device",
                        principalColumn: "DeviceId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Device",
                columns: new[] { "DeviceId", "DeviceName", "Latitude", "Longitude", "StudentNumber" },
                values: new object[] { new Guid("0cd895f4-6713-41ab-b7dc-3229bacf30a9"), "test", 52.0, 4.5, "hjdcbs" });

            migrationBuilder.InsertData(
                table: "Device",
                columns: new[] { "DeviceId", "DeviceName", "Latitude", "Longitude", "StudentNumber" },
                values: new object[] { new Guid("b569148b-19ff-4fa9-9c8b-2d8d41223704"), "test", 52.0, 5.0, "bla" });

            migrationBuilder.InsertData(
                table: "WeatherUpdate",
                columns: new[] { "WeatherUpdateId", "DeviceId", "Humidity", "TemperatureC", "TimeStamp", "Windspeed" },
                values: new object[] { new Guid("038ae1f2-18df-41a4-9e57-03450a0dab7d"), new Guid("0cd895f4-6713-41ab-b7dc-3229bacf30a9"), 5.0, 10.0, new DateTime(2018, 11, 7, 19, 44, 51, 801, DateTimeKind.Local), 5.0 });

            migrationBuilder.InsertData(
                table: "WeatherUpdate",
                columns: new[] { "WeatherUpdateId", "DeviceId", "Humidity", "TemperatureC", "TimeStamp", "Windspeed" },
                values: new object[] { new Guid("d7dff818-22a2-418e-a696-8875748afe98"), new Guid("b569148b-19ff-4fa9-9c8b-2d8d41223704"), 5.0, 10.0, new DateTime(2018, 11, 7, 19, 44, 51, 804, DateTimeKind.Local), 5.0 });

            migrationBuilder.CreateIndex(
                name: "IX_WeatherUpdate_DeviceId",
                table: "WeatherUpdate",
                column: "DeviceId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WeatherUpdate");

            migrationBuilder.DropTable(
                name: "Device");
        }
    }
}
