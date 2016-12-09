#!bin/bash

# docker run --name mongo -p 27017:27017 -v $PWD/data:/data/db -d mongo


# dotnet restore
# dotnet test test/myRetail.Tests/project.json
# dotnet publish src/myRetail/project.json -c release -o $(pwd)/publish/
# docker build -t myretail publish
# docker stop myRetail || true && docker rm myRetail || true
# docker run -d --name myRetail --link mongo:mongo -p 8001:80 myretail


#docker build -t mongo-seed mongo-seed
#docker run --name mongo-seed --link mongo:mongo mongo-seed


dotnet restore
dotnet publish src/myRetail/project.json -c release -o $(pwd)/publish/
docker build -t myretail publish
docker stop myRetail || true && docker rm myRetail || true
docker run -d --name myRetail --link mongo:mongo -p 8001:80 myretail

