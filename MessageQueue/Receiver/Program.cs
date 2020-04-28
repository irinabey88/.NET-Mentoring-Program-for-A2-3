using RabbitMqQueue;
using System;

namespace Receiver
{
    class Program
    {
        static void Main(string[] args)
        {
            // Wait for the user to quit the program.
            Console.WriteLine("Press 'q' to quit the program.");

            var rabbitMqReceiver = new RabbitMqReceiver();
            rabbitMqReceiver.StartListenToMessages();

            while (Console.Read() != 'q') ;
        }       
    }
}
