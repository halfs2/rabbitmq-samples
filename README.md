# RabbitMQ Samples
## Requirements
- NET 5
- Docker

## How to run
- Create RabbitMQ in docker
```sh
docker run -d --hostname my-rabbit --name some-rabbit -p 15672:15672 -p 5672:5672 rabbitmq:management
```
- Hello World
```sh
## first run receive
cd YOUR_PATH_HERE\rabbitmq-samples-main\src\HelloWorld
dotnet run --project Receive/Receive.csproj

## now run send and type your messages to send
dotnet run --project Send/Send.csproj
```

- Work Queues
```sh
## first run two or more workers
cd YOUR_PATH_HERE\rabbitmq-samples-main\src\WorkQueues
dotnet run --project Worker/Worker.csproj

## now run a new task passing a message as a parameter. Each dot equals 1 second of processing.
dotnet run --project NewTask/NewTask.csproj "task 1 ."
dotnet run --project NewTask/NewTask.csproj "task 2 .."
dotnet run --project NewTask/NewTask.csproj "task 3 ..."
```
