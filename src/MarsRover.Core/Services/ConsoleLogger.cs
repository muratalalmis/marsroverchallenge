using System;

namespace MarsRover.Core.Services
{
    public class ConsoleLogger : ILogger
    {
        public void Error(Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

        public void Info(string message)
        {
            Console.WriteLine(message);
        }
    }
}
