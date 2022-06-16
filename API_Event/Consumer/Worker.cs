using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace Consumer
{
    public class Worker
    {
        public void InitializeWorker()
        {
            var factory = new ConnectionFactory()
            {
                HostName = "localhost"
            };

            using(var connection = factory.CreateConnection())
            using(var channel = connection.CreateModel())
            {
                channel.QueueDeclare(
                    queue: "rabbit_queue",
                    durable: false,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null);

                channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);

                Console.WriteLine("[*] Waiting for messages.");

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (sender, args) =>
                {
                    try
                    {
                        var body = args.Body.ToArray();
                        var message = Encoding.UTF8.GetString(body);
                        Console.WriteLine($" [*] Received {message}");

                        var dots = message.Split('.').Length - 1;
                        Thread.Sleep(dots * 1000);

                        Console.WriteLine(" [x] Done");

                        channel.BasicAck(deliveryTag: args.DeliveryTag, multiple: false);
                    }
                    catch (Exception)
                    {
                        channel.BasicNack(args.DeliveryTag, false, true);
                    }
                };

                channel.BasicConsume(
                    queue: "rabbit_queue",
                    autoAck: false,
                    consumer: consumer);

                Console.WriteLine(" Press [enter] to exit.");
                Console.ReadLine();

            }
        }
    }
}
