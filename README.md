# Weather station server for Smart Things Minor
This repository contains the back-end server for all the ESP8266 Weather staions.

## Prerequisites
- .NET Core 2.1

## Installation
1. `cd` to the `weather.station.server/weather.station.server` directory
2. Initialize a local SqLite database with `dotnet ef database update`
3. You are now ready to use the application and start it with `dotnet run`

## Migrations
Changes to the database can be applied with migrations. Run the command `dotnet ef database update` to apply migrations. Create a new migration with the command `dotnet ef migrations add <name>` when you maken changs to the database structure. Migrations are required to deploy the application, without a deployment will fail!