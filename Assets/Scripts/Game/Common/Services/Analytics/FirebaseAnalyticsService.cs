using Firebase.Analytics;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Firebase;

namespace Game.Common.Services.Analytics
{
    
public class FirebaseAnalyticsService : IAnalyticsService
{
    private ILoggingService _loggingService;
    private bool _isInitialized;
    
    public FirebaseAnalyticsService(ILoggingService loggingService)
    {
        _loggingService = loggingService;
    }
    
    private void Initialize()
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
        {
            var dependencyStatus = task.Result;
            if (dependencyStatus == DependencyStatus.Available)
            {
                InitializeFirebase();
                _loggingService.Log("Firebase Analytics initialized successfully");
            }
            else
            {
                _loggingService.LogError($"Could not resolve all Firebase dependencies: {dependencyStatus}");
            }
        });
    }

    private void InitializeFirebase()
    {
        _isInitialized = true;
    }

    public void LogEvent(string eventName)
    {
        if (!_isInitialized)
        {
            _loggingService.LogWarning("Firebase Analytics not initialized yet");
            return;
        }
        
        FirebaseAnalytics.LogEvent(eventName);
    }

    public void LogEvent(string eventName, string parameterName, string parameterValue)
    {
        if (!_isInitialized)
        {
            _loggingService.LogWarning("Firebase Analytics not initialized yet");
            return;
        }
        
        FirebaseAnalytics.LogEvent(eventName, parameterName, parameterValue);
    }

    public void LogEvent(string eventName, Dictionary<string, object> parameters)
    {
        if (!_isInitialized)
        {
            _loggingService.LogWarning("Firebase Analytics not initialized yet");
            return;
        }
        
        Parameter[] firebaseParameters = new Parameter[parameters.Count];
        int index = 0;
        
        foreach (var parameter in parameters)
        {
            if (parameter.Value is string stringValue)
            {
                firebaseParameters[index] = new Parameter(parameter.Key, stringValue);
            }
            else if (parameter.Value is long longValue)
            {
                firebaseParameters[index] = new Parameter(parameter.Key, longValue);
            }
            else if (parameter.Value is double doubleValue)
            {
                firebaseParameters[index] = new Parameter(parameter.Key, doubleValue);
            }
            else if (parameter.Value is int intValue)
            {
                firebaseParameters[index] = new Parameter(parameter.Key, intValue);
            }
            else
            {
                firebaseParameters[index] = new Parameter(parameter.Key, parameter.Value.ToString());
            }
            
            index++;
        }
        
        FirebaseAnalytics.LogEvent(eventName, firebaseParameters);
    }

    public void SetUserProperty(string propertyName, string propertyValue)
    {
        if (!_isInitialized)
        {
            _loggingService.LogWarning("Firebase Analytics not initialized yet");
            return;
        }
        
        FirebaseAnalytics.SetUserProperty(propertyName, propertyValue);
    }

    public void SetUserId(string userId)
    {
        if (!_isInitialized)
        {
            _loggingService.LogWarning("Firebase Analytics not initialized yet");
            return;
        }
        
        FirebaseAnalytics.SetUserId(userId);
    }

    public UniTask InitializeAsync()
    {
        Initialize();
        return UniTask.CompletedTask;
    }
}

}