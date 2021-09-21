# RabbitMQ Samples
## Requirements
- NET 5
- Docker

## How to run
- Create RabbitMQ in docker 
```sh
docker run -d --hostname my-rabbit --name some-rabbit -p 15672:15672 -p 5672:5672 rabbitmq:management
```
> as the image we installed has the management tag, you can access a web dashboard to view your queues, messages, connections etc. access here http://localhost:15672

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

- Publish Subscribe
```sh
## first run two or more workers
cd YOUR_PATH_HERE\rabbitmq-samples-main\src\PublishSubscribe
## this project save messages in to file logs_from_rabbit.log
dotnet run --project ReceiveLogs/ReceiveLogs.csproj > logs_from_rabbit.log
## this project print messages in console
dotnet run --project ReceiveLogs/ReceiveLogs.csproj

## now run a emitlog
dotnet run --project EmitLog/EmitLog.csproj "log # 1"
dotnet run --project EmitLog/EmitLog.csproj "log # 2"
dotnet run --project EmitLog/EmitLog.csproj "log # 3"
```

- Routing
```sh
cd YOUR_PATH_HERE\rabbitmq-samples-main\src\Routing
## this project save messages in to file logs_from_rabbit.log
dotnet run --project ReceiveLogsDirect/ReceiveLogsDirect.csproj error warning > logs_from_rabbit.log
## this project print messages in console
dotnet run --project ReceiveLogsDirect/ReceiveLogsDirect.csproj info

## now run a emitlog with level error in parameter
dotnet run --project EmitLogDirect/EmitLogDirect.csproj error "this error will be saved to a file"
dotnet run --project EmitLogDirect/EmitLogDirect.csproj warning "this warning will be saved to a file"
dotnet run --project EmitLogDirect/EmitLogDirect.csproj info "this message will be print to a console"
```

- Topics
```sh
cd YOUR_PATH_HERE\rabbitmq-samples-main\src\Topics
## Consumers
dotnet run --project ReceiveLogsTopic/ReceiveLogsTopic.csproj "#"

dotnet run --project ReceiveLogsTopic/ReceiveLogsTopic.csproj "kern.*"

dotnet run --project ReceiveLogsTopic/ReceiveLogsTopic.csproj "*.critical"

dotnet run --project ReceiveLogsTopic/ReceiveLogsTopic.csproj "kern.*" "*.critical"

## Publisher
dotnet run --project EmitLogTopic/EmitLogTopic.csproj "kern.critical" "A critical kernel error"
```
