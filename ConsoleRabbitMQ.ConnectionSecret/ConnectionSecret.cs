using RabbitMQ.Client;
using System;

namespace ConsoleRabbitMQ.Receiver
{
    //Это в gitignore, но для наглядности подключения оставил здесь, userName and password по требованию
    public static class ConnectionSecret
    {
        public const string UserName = "";
        public const string Password = "";

        public static ConnectionFactory GetConnectionFactory()
        {
            ConnectionFactory factory = new ConnectionFactory();
            factory.Uri = new Uri($"amqps://{UserName}:{Password}@snake.rmq2.cloudamqp.com/bublpzgh");
            factory.AutomaticRecoveryEnabled = true;

            return factory;
        }
    }
}
