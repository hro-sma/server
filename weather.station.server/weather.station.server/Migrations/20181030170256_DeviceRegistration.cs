using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace weather.station.server.Migrations
{
    public partial class DeviceRegistration : Migration
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
                  
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Device", x => x.DeviceId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Device");
        }
    }
}
