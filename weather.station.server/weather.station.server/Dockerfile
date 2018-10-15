FROM microsoft/dotnet:2.1-sdk AS build-env
ENV ASPNETCORE_ENVIRONMENT Production
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY *.csproj ./
RUN dotnet restore

# Copy everything else and build
COPY . ./
RUN dotnet publish -c Release -o out

# Build runtime image
FROM microsoft/dotnet:2.1-aspnetcore-runtime
ENV ASPNETCORE_ENVIRONMENT Production

WORKDIR /app
COPY --from=build-env /app/out .
ENV ASPNETCORE_URLS=http://+:5000
ENTRYPOINT ["dotnet", "weather.station.server.dll"]