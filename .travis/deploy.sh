#!/bin/bash

eval "$(ssh-agent -s)" # Start ssh-agent cache
chmod 600 .travis/id_rsa # Allow read access to the private key
ssh-add .travis/id_rsa # Add the private key to SSH

# Skip this command if you don't need to execute any additional commands after deploying.
ssh deploy@$IP -p $PORT <<EOF
  cd ./weather.station.server
  git fetch origin
  git reset --hard origin/master
  git pull
  cd ./weather.station.server/weather.station.server
  docker build --force-rm -t weatherserver .
  docker stop weather-station-server || true
  docker rm --force weather-station-server || true
  docker run --restart=always --net=host --name=weather-station-server -d -t weatherserver
EOF