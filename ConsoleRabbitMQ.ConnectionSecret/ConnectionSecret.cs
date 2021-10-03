using RabbitMQ.Client;
using System;

namespace ConsoleRabbitMQ.Receiver
{
    public static class ConnectionSecret
    {
        public const string UserName = "bublpzgh";
        public const string Password = "yuWCvFYxC_GzUtu9bvlL42jXH2Z54DHL";

        public static ConnectionFactory GetConnectionFactory()
        {
            ConnectionFactory factory = new ConnectionFactory();
            factory.Uri = new Uri($"amqps://{UserName}:{Password}@snake.rmq2.cloudamqp.com/bublpzgh");
            factory.AutomaticRecoveryEnabled = true;

            return factory;
        }
    }
}
