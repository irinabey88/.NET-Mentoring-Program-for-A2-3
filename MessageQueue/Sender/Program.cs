using RabbitMqQueue;
using System;
using System.IO;
using System.Security.Permissions;

namespace Sender
{
    class Program
    {
        private const string WatchDirectoryPath = "C:\\Test";

        static void Main(string[] args)
        {
            //Create queue if not exists
            var queueCreator = new QueueCreator();
            queueCreator.Create();

            Run();
        }

        [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
        private static void Run()
        {
            // Create a new FileSystemWatcher and set its properties.
            using (FileSystemWatcher watcher = new FileSystemWatcher())
            {
                watcher.Path = WatchDirectoryPath;

                // Only watch text files.
                watcher.Filter = "*.txt";

                // Add event handlers.
                watcher.Created += OnCreate;

                // Begin watching.
                watcher.EnableRaisingEvents = true;

                // Wait for the user to quit the program.
                Console.WriteLine("Press 'q' to quit the program.");
                while (Console.Read() != 'q') ;
            }
        }

        // Define the event handlers.
        private static void OnCreate(object source, FileSystemEventArgs e)
        {
            var sender = new RabbitMqSender();
            sender.SendFile(e.FullPath);
        }
    }
}
