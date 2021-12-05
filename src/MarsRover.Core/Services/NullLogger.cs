using System;

namespace MarsRover.Core.Services
{
    public class NullLogger : ILogger
    {
        public void Error(Exception ex)
        {
        }

        public void Info(string message)
        {
        }
    }
}
