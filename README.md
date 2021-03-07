# Inspiring GRPC
Inspiring to use gRPC in .NET 5 with an easy example

## Docker
Build docker image with the next command
```zsh
docker build --build-arg DEPLOYMENT_ENVIRONMENT=Production -t inspiring-grpc/server:v1.0 .
```

Run docker image on a container with detached mode
```zsh
docker run -d --rm -p 5000:80 --name inspiring_grpc inspiring-grpc/server:v1.0
```

Show container logs
```zsh
docker logs -f inspiring_grpc
```