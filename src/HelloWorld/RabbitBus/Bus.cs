using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace RabbitBus
{
    public class Bus
    {
        private readonly ConnectionFactory _factory;
        public Bus(string hostName, int port)
        {
            _factory = new ConnectionFactory() { HostName = "localhost", Port = 5672 };
        }

        public void Send(string queue, string message, bool durable = false, bool persistent = false)
        {
            using (var connection = _factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue: queue, durable: durable, exclusive: false, autoDelete: false, arguments: null);
                    
                    var body = Encoding.UTF8.GetBytes(message);

                    var properties = channel.CreateBasicProperties();
                    properties.Persistent = persistent;

                    channel.BasicPublish(exchange: "", routingKey: queue, basicProperties: properties, body: body);

                    Console.WriteLine("[x] sent {0} to queue {1}", message, queue);
                }
            }
        }

        public void Receive(string queue, bool durable = false)
        {
            using (var connection = _factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue: queue, durable: durable, exclusive: false, autoDelete: false, arguments: null);

                    channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);

                    Console.WriteLine(" [*] Waiting for messages.");

                    var consumer = new EventingBasicConsumer(channel);
                    consumer.Received += (model, ea) =>
                    {
                        var body = ea.Body.ToArray();
                        var message = Encoding.UTF8.GetString(body);
                        Console.WriteLine(" [x] Received {0}", message);

                        int dots = message.Split('.').Length - 1;
                        Thread.Sleep(dots * 1000);

                        Console.WriteLine(" [x] Done");

                        channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
                    };

                    channel.BasicConsume(queue: queue,
                                         autoAck: false,
                                         consumer: consumer);

                    Console.WriteLine(" Press [enter] to exit.");
                    Console.ReadLine();
                }
            }
        }
    }
}
