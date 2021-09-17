using System;
using RabbitBus;


namespace NewTask
{
    class Program
    {
        static void Main(string[] args)
        {
            var bus = new Bus(hostName: "localhost", port: 5672);

            var message = GetMessage(args);

            bus.Send(queue: "task_queue", message: message, durable: true, persistent: true);
        }

        private static string GetMessage(string[] args)
        {
            return ((args.Length > 0) ? string.Join(" ", args) : "Hello World!");
        }
    }
}
