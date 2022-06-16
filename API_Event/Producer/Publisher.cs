using RabbitMQ.Client;
using System.Text;

namespace Producer
{
    public class Publisher
    {
        public void InitializePublisher(string[] args)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(
                    queue: "rabbit_queue",
                    exclusive: false,
                    autoDelete: false,
                    arguments: null);

                var message = GetMessage(args);
                var body = Encoding.UTF8.GetBytes(message);

                var properties = channel.CreateBasicProperties();
                properties.Persistent = true;

                channel.BasicPublish(
                    exchange: "",
                    routingKey: "rabbit_queue",
                    basicProperties: properties,
                    body: body);

                Console.WriteLine($" [x] Sent {message}");
            }

            Console.WriteLine(" Press [enter] to exit");
            Console.ReadLine();
        }

        private static string GetMessage(string[] args)
        {
            return ((args.Length > 0) ? string.Join(" ", args) : "No message found.");
        }
    }
}