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
        sh '''docker stop $(docker ps -q --filter ancestor=weatherserver)
docker run --restart=always --net=host -d -t weatherserver'''
      }
    }
  }
}