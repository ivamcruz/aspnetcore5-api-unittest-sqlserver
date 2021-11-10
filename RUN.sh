
#!/bin/bash
dotnet test src/Portal.DevTest.Test

docker-compose build

docker-compose up -d

APIDOC=$(docker inspect --format='http://localhost:{{(index (index .NetworkSettings.Ports "80/tcp") 0).HostPort}}/swagger' portal-telemedicina-api)

echo Access API documentation $APIDOC

start chrome $APIDOC