#!/usr/bin/env groovy

node ('bargelt_dotnetcore_myretail') {
	stage('Build') {    
		git url: 'https://github.com/stevebargelt/myRetail'

		sh 'dotnet restore'
		sh 'dotnet test test/myRetail.Tests/project.json'
	}
	stage('Publish') {
		sh 'dotnet publish src/myRetail/project.json -c release -o $(pwd)/publish/'
		echo "Building: ${env.BUILD_TAG} || Build Number: ${env.BUILD_NUMBER}"
		sh "docker build -t abs-registry.harebrained-apps.com/myretail:${env.BUILD_NUMBER} publish"
		withCredentials([usernamePassword(credentialsId: 'absadmin', passwordVariable: 'REGISTRY_PASSWORD', usernameVariable: 'REGISTRY_USER')]) {
			sh "docker login abs-registry.harebrained-apps.com -u='${REGISTRY_USER}' -p='${REGISTRY_PASSWORD}'"
		}
    	sh "docker push abs-registry.harebrained-apps.com/myretail:${env.BUILD_NUMBER}"
	}
	stage('Production') {
		withEnv([
			"DOCKER_TLS_VERIFY=1",
			"DOCKER_HOST=tcp://abs.harebrained-apps.com:2376",
			"DOCKER_CERT_PATH=/usr/local/etc/jenkins/certs/"
		]) {
			sh "docker pull abs-registry.harebrained-apps.com/myretail:${env.BUILD_NUMBER}"
			sh "docker stop myRetail || true && docker rm myRetail || true"
			sh "docker run -d --name myRetail -p 8001:80 abs-registry.harebrained-apps.com/myretail:${env.BUILD_NUMBER}"
		}
	}
} //node