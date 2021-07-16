using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;

namespace ConsoleRabbitMQ.Recivier
{
    class Program
    {
        public static void Main(string[] args)
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
