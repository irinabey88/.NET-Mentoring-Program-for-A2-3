using RabbitMQ.Client;

namespace RabbitMqQueue
{
    public class QueueCreator
    {
        public void Create()
        {
            var connectionFactory = new ConnectionFactory
            {
                HostName = QueueConstants.Host,
                UserName = QueueConstants.User,
                Password = QueueConstants.Password
            };

            var connection = connectionFactory.CreateConnection();
            var model = connection.CreateModel();
            var properties = model.CreateBasicProperties();
            properties.Persistent = true;

           //queue.declare is an idempotent operation.
           model.QueueDeclare(QueueConstants.QueueName, true, false, false, null);
        }
    }
}
