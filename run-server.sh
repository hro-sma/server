#!/bin/bash
git pull
docker-compose -f weather.station.server/weather.station.server/docker-compose.yml down
docker-compose -f weather.station.server/weather.station.server/docker-compose.yml up -d