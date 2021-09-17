using System;
using RabbitBus;

namespace Worker
{
    class Program
    {
        static void Main(string[] args)
        {
            var bus = new Bus(hostName: "localhost", port: 5672);

            bus.Receive(queue: "task_queue", durable: true);
        }

    }
}