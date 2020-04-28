using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.IO;

namespace RabbitMqQueue
{
    public class RabbitMqSender
    {
        private const int ChunkSize = 4098;

        public void SendFile(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
            {
                throw new ArgumentNullException($"Invalid file name", nameof(filePath));
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
                var fileName = $"{Guid.NewGuid()}";

                using (var fileStream = File.OpenRead(filePath))
                {
                    using (var streamReader = new StreamReader(fileStream))
                    {
                        byte[] buffer;
                        int remainingSize = (int)fileStream.Length;
                        int length = (int)fileStream.Length;
                        var messageCount = 0;
                        var isEndOfSequence = false;

                        while (remainingSize > 0)
                        {
                            //Read Chunk
                            int read;
                            if (remainingSize > ChunkSize)
                            {
                                buffer = new byte[ChunkSize];
                                read = fileStream.Read(buffer, 0, ChunkSize);
                            }
                            else
                            {
                                buffer = new byte[remainingSize];
                                read = fileStream.Read(buffer, 0, remainingSize);
                                isEndOfSequence = true;

                            }

                            //Setup properties
                            Dictionary<string, object> headers = new Dictionary<string, object>();
                            headers.Add("FileName", fileName);
                            headers.Add("SequenceNumber", messageCount);
                            headers.Add("IsEndOfSequence", isEndOfSequence);

                            var properties = model.CreateBasicProperties();
                            properties.Persistent = true;
                            properties.Headers = headers;

                            //Send message
                            Console.WriteLine($"Sending Chunk message - Index = {messageCount} Length = {read}");
                            model.BasicPublish(QueueConstants.ExcangeName, QueueConstants.QueueName, properties, buffer);

                            messageCount++;
                            remainingSize = remainingSize - read;
                        }

                        Console.WriteLine($"Completed sending {messageCount} chunks");
                    }
                }
            }
        }
    }
}
