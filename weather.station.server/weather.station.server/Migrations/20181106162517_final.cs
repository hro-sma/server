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
                values: new object[] { new Guid("b7886e5a-515f-4c60-b45c-ecf9add01ea6"), "test", 52.0, 4.5, "hjdcbs" });

            migrationBuilder.InsertData(
                table: "Device",
                columns: new[] { "DeviceId", "DeviceName", "Latitude", "Longitude", "StudentNumber" },
                values: new object[] { new Guid("750de99f-c0d1-458e-8d26-ff4237bcdfd2"), "test", 52.0, 5.0, "bla" });

            migrationBuilder.InsertData(
                table: "WeatherUpdate",
                columns: new[] { "WeatherUpdateId", "DeviceId", "Humidity", "TemperatureC", "TimeStamp", "Windspeed" },
                values: new object[] { new Guid("a598b97e-162d-4b68-a2a9-4e831d0fc90d"), new Guid("b7886e5a-515f-4c60-b45c-ecf9add01ea6"), 5.0, 10.0, new DateTime(2018, 11, 6, 17, 25, 17, 209, DateTimeKind.Local), 5.0 });

            migrationBuilder.InsertData(
                table: "WeatherUpdate",
                columns: new[] { "WeatherUpdateId", "DeviceId", "Humidity", "TemperatureC", "TimeStamp", "Windspeed" },
                values: new object[] { new Guid("e5fd6bb4-792b-44fc-8966-9e50b663ebe3"), new Guid("750de99f-c0d1-458e-8d26-ff4237bcdfd2"), 5.0, 10.0, new DateTime(2018, 11, 6, 17, 25, 17, 210, DateTimeKind.Local), 5.0 });

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
