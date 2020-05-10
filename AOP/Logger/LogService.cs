using Logger.Interfaces;
using System;
using System.IO;

namespace Logger
{
    public class LogService : ILogger
    {
        private readonly string _path;

        public LogService(string path)
        {
            if(string.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentException($"Path can not be null, empty string or contains only white spaces");
            }

            _path = path;
            if(!Directory.Exists(_path))
            {
                Directory.CreateDirectory(path);
            }
        }

        public void LogInfo(string data)
        {
            var fileName = $"{_path}//{DateTime.Now.Date.ToShortDateString().Replace('.', '-')}.log";
            using (var streamWriter = File.AppendText(fileName))
            {
                streamWriter.WriteLine(data);
            }
        }
    }
}
