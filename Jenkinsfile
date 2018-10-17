pipeline {
  agent any
  stages {
    stage('Settings') {
      steps {
        sh 'cp /home/envs/weather-station-server/appsettings.json ./weather.station.server/weather.station.server/appsettings.json'
      }
    }
    stage('Build') {
      steps {
        sh '''cd ./weather.station.server/weather.station.server
docker build --force-rm -t weatherserver .'''
      }
    }
    stage('Restart') {
      steps {
        sh '''docker stop weather-station-server
docker run --restart=always --net=host --name=weather-station-server -d -t weatherserver'''
      }
    }
  }
}