using ConsoleRabbitMQ.Receiver;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleRabbitMQ.Recivier
{
    class Program
    {
        private static IConnection connection;
        private static IModel channelPost;
        private static IModel channelPut;
        public static void Main(string[] args)
        {
            try
            {
                initializationConnections(ConnectionSecret.GetConnectionFactory());
                do
                {
                    listenPostQue();
                    listenPutQue();

                } while (Console.ReadKey(true).Key != ConsoleKey.Escape);
            }
            catch (Exception e)
            {
                Console.WriteLine($" Error {e.Message}");
            }
            finally
            {
                closeConnections();
            }
        }

        private static void initializationConnections(ConnectionFactory factory)
        {
            connection = factory.CreateConnection();
            channelPost = connection.CreateModel();
            channelPut = connection.CreateModel();
        }

        private static void closeConnections()
        {
            connection?.Close();
            channelPost?.Close();
            channelPut?.Close();
        }

        private static void listenPostQue()
        {
            channelPost.QueueDeclare(queue: "Hello",
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);
            var consumerPost = new AsyncEventingBasicConsumer(channelPost);
            consumerPost.Received += async (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine(" [x] Received {0}", message);
                Console.WriteLine($"Answer from Client - {await WebApi.PostAsync("http://localhost:5500/api/Queue", message)}");
            };
            channelPost.BasicConsume(queue: "Hello",
                                 autoAck: true,
                                 consumer: consumerPost);
        }

        private static void listenPutQue()
        {
            channelPost.QueueDeclare(queue: "Bye",
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);
            var consumerPost = new AsyncEventingBasicConsumer(channelPost);
            consumerPost.Received += async (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine(" [x] Received {0}", message);
                Console.WriteLine($"Answer from Client - {await WebApi.PutAsync("http://localhost:5500/api/Queue", message)}");
            };
            channelPost.BasicConsume(queue: "Bye",
                                 autoAck: true,
                                 consumer: consumerPost);
        }
          
    }


}
