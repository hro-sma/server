#!/bin/bash
set -e

eval "$(ssh-agent -s)"
chmod 600 deploy_rsa
ssh-add deploy_rsa

# Skip this command if you don't need to execute any additional commands after deploying.
ssh deploy@$IP -p $PORT <<EOF
  set -e
  cd weather-station-server
  git fetch origin
  git reset --hard origin/master
  git pull
  cd ./weather.station.server/weather.station.server
  docker build --force-rm -t weatherserver .
  docker stop weather-station-server || true
  docker rm --force weather-station-server || true
  docker run --restart=always --net=host --name=weather-station-server -d -t weatherserver
EOF