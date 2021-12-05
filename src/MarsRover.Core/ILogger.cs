using System;

namespace MarsRover.Core
{
    public interface ILogger
    {
        void Info(string message);
        void Error(Exception ex);
    }
}