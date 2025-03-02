namespace Game.Common
{
    public interface ILoggingService
    {
        public void Log(string message);
        public void LogError(string message);
        public void LogWarning(string message);
    }
}