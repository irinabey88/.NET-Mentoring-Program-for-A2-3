using RabbitMqQueue;
using System;

namespace Receiver
{
    class Program
    {
        static void Main(string[] args)
        {
            //Create queue if not exists
            var queueCreator = new QueueCreator();
            queueCreator.Create();

            var rabbitMqReceiver = new RabbitMqReceiver();
            rabbitMqReceiver.StartListenToMessages();
        }       
    }
}
