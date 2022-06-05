using System;

namespace WatsonSmartHome.Logging
{
    public interface ILoggingService
    {
        void LogInformation(string message);
        void LogError(string message, Exception exception);
        void LogFatal(string message, Exception exception);
        void LogDebug(string message);
        void LogVerbose(string message);
    }
}