using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace HelloBus
{
    public class Bus
    {
        private readonly ConnectionFactory _factory;
        public Bus(string hostName, int port)
        {
            _factory = new ConnectionFactory() { HostName = "localhost", Port = 5672 };
        }

        public void Send(string queue, string message)
        {
            using (var connection = _factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue: queue, durable: false, exclusive: false, autoDelete: false, arguments: null);
                    
                    var body = Encoding.UTF8.GetBytes(message);

                    channel.BasicPublish(exchange: "", routingKey: queue, basicProperties: null, body: body);

                    Console.WriteLine("[x] sent {0} to queue {1}", message, queue);
                }
            }
        }

        public void Receive(string queue)
        {
            using (var connection = _factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue: queue, durable: false, exclusive: false, autoDelete: false, arguments: null);

                    var consumer = new EventingBasicConsumer(channel);
                    consumer.Received += (model, ea) =>
                    {
                        var body = ea.Body.ToArray();
                        var message = Encoding.UTF8.GetString(body);
                        Console.WriteLine(" [x] Received {0}", message);
                    };

                    channel.BasicConsume(queue: "hello",
                                         autoAck: true,
                                         consumer: consumer);

                    Console.WriteLine(" Press [enter] to exit.");
                    Console.ReadLine();
                }
            }
        }
    }
}
