using System;
using RabbitMQ.Client;
using System.Text;
using ConsoleRabbitMQ.Receiver;

namespace ConsoleRabbitMQ.Sender
{
    class Send
    {
        private static IConnection connection;
        private static IModel channelPost;
        private static IModel channelPut;

        public static void Main()
        {
            try
            {
                initializationConnections(ConnectionSecret.GetConnectionFactory());
                do
                {
                    Console.WriteLine(" Select a sending method : 1-hello, 2-bye");
                    var answerUser= Console.ReadLine();
                    Console.WriteLine(choiceUseCase(answerUser));

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

        private static void sendMessageToPostQueue(string message)
        {
            var body = Encoding.UTF8.GetBytes(message);

            channelPost.BasicPublish(exchange: "",
                                 routingKey: "Hello",
                                 basicProperties: null,
                                 body: body);
            Console.WriteLine(" [x] Sent {0}", message);
        }

        private static void sendMessageToPutQueue(string message)
        {
            var body = Encoding.UTF8.GetBytes(message);

            channelPut.BasicPublish(exchange: "",
                                 routingKey: "Bye",
                                 basicProperties: null,
                                 body: body);
            Console.WriteLine(" [x] Sent {0}", message);
        }

        private static string choiceUseCase(string answerUser)
        {
            switch (answerUser)
            {
                case "1":
                    sendMessageToPostQueue("Hello my friend!!!");
                    return "Message 1 is sended";
                case "2":
                    sendMessageToPutQueue("Bye my friend!!!");
                    return "Message 2 is sended";
                default:
                    return "Not correct input text";
            }
        }
        
        private static void closeConnections()
        {
            connection?.Close();
            channelPost?.Close();
            channelPut?.Close();
        }

        private static void initializationConnections(ConnectionFactory factory)
        {
            connection = factory.CreateConnection();
            channelPost = connection.CreateModel();
            channelPut = connection.CreateModel();
        }
    }
}
