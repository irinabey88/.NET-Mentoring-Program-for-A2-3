using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.IO;
using System.Text;

namespace RabbitMqQueue
{
    public class RabbitMqReceiver
    {
        private const string ReceiverPath = "C:\\TestResult";

        public void StartListenToMessages()
        {
            if (!Directory.Exists(ReceiverPath))
            {
                Directory.CreateDirectory(ReceiverPath);
            }

            var factory = new ConnectionFactory()
            {
                HostName = QueueConstants.Host,
                UserName = QueueConstants.User,
                Password = QueueConstants.Password
            };

            using (var connection = factory.CreateConnection())
            using (var model = connection.CreateModel())
            {
                model.BasicQos(0, 1, false);
                var consumer = new EventingBasicConsumer(model);
                model.BasicConsume(QueueConstants.QueueName, false, consumer);

                Console.WriteLine("Started and listening for a message");
                consumer.Received += (obj, eventArgs) =>
                {
                    HandleMessage(model, eventArgs);
                };
                while (true)
                {

                }
            }
        }

        private static void HandleMessage(IModel model, BasicDeliverEventArgs eventArgs)
        {
            Console.WriteLine("Received message");

            //Get file
            var body = eventArgs.Body.ToArray();
            var headers = eventArgs.BasicProperties.Headers;

            var pathProperty = (byte[])headers["FileName"];
            var inprogressPass = Encoding.Default.GetString(pathProperty);
            var sequenceNumber = (int)headers["SequenceNumber"];
            var isEndOfSequence = (bool)headers["IsEndOfSequence"];

            inprogressPass = $"{ReceiverPath}\\{inprogressPass}.inprogres";

            //Adding message
            using (var fileStream = new FileStream(inprogressPass, FileMode.Append, FileAccess.Write))
            {
                fileStream.Write(body, 0, body.Length);
                fileStream.Flush();
            }
            Console.WriteLine($"Message saved to disk - sequence number = {sequenceNumber}");

            if (isEndOfSequence)
            {
                Console.WriteLine("Received last message");
                //Renaming file
                var completedPath = Path.ChangeExtension(inprogressPass, ".completed");
                File.Move(inprogressPass, completedPath);
                File.Delete(inprogressPass);
            }

            model.BasicAck(eventArgs.DeliveryTag, false);
            Console.WriteLine("Listening to another message");
        }
    }
}
