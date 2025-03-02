using UnityEngine;

namespace Game.Common.Services.Logging
{
    public class ConsoleLoggingService : ILoggingService
    {
        public void Log(string message)
        {
            Debug.Log(message);
        }

        public void LogError(string message)
        {
            Debug.LogError(message);
        }

        public void LogWarning(string message)
        {
            Debug.LogWarning(message);
        }
    }
}