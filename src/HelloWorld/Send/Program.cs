using RabbitBus;
using System;


namespace Send
{
    class Program
    {
        static void Main(string[] args)
        {
            var bus = new Bus(hostName: "localhost", port: 5672);

            while (true)
            {
                Console.WriteLine("type CTRL+C to exit.");
                Console.WriteLine("type your message: ");
                
                var message = Console.ReadLine();
                bus.Send(queue: "hello", message);
            }
        }
    }
}
