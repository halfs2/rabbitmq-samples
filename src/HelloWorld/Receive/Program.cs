using HelloBus;
using System;

namespace Receive
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("receiving messages");
            var bus = new Bus(hostName: "localhost", port: 5672);

            bus.Receive("hello");
        }
    }
}
