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
                values: new object[] { new Guid("1ad80c92-847a-45dd-8397-43f86b417bd9"), "test", 5.0, 5.0, "hjdcbs" });

            migrationBuilder.InsertData(
                table: "WeatherUpdate",
                columns: new[] { "WeatherUpdateId", "DeviceId", "Humidity", "TemperatureC", "TimeStamp", "Windspeed" },
                values: new object[] { new Guid("937272a5-3f82-41ec-80af-395d39690be7"), new Guid("1ad80c92-847a-45dd-8397-43f86b417bd9"), 5.0, 5.0, new DateTime(2018, 11, 6, 16, 58, 58, 715, DateTimeKind.Local), 5.0 });

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
