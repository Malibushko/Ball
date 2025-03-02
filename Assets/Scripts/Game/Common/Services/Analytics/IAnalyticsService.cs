using System.Collections.Generic;
using Core.Infrastructure.Interfaces;

namespace Game.Common.Services.Analytics
{
    public interface IAnalyticsService : IInitializableAsyncService
    {
        void LogEvent(string eventName);
        void LogEvent(string eventName, string parameterName, string parameterValue);
        void LogEvent(string eventName, Dictionary<string, object> parameters);
        void SetUserProperty(string propertyName, string propertyValue);
        void SetUserId(string userId);
    }
}