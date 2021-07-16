using System;
using RabbitMQ.Client;
using System.Text;

namespace ConsoleRabbitMQ.Sender
{
    class Send
    {
        public static void Main()
        {
            ConnectionFactory factory = new ConnectionFactory();
            factory.Uri = new Uri("amqps://bublpzgh:M7fqfVEcZeB_LeBPkXanh8ZajkyGxfHq@snake.rmq2.cloudamqp.com/bublpzgh");
            factory.AutomaticRecoveryEnabled = true;
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "hello",
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                string message = "Hello World!";
                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(exchange: "",
                                     routingKey: "hello",
                                     basicProperties: null,
                                     body: body);
                Console.WriteLine(" [x] Sent {0}", message);
            }

            Console.WriteLine(" Press [enter] to exit.");
            Console.ReadLine();
        }
    }
}
