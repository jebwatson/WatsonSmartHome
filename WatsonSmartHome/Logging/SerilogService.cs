using System;
using Serilog;

namespace WatsonSmartHome.Logging
{
    public class SerilogService : ILoggingService
    {
        public SerilogService()
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .WriteTo.Console()
                .WriteTo.File(
                    "log.txt",
                    rollingInterval: RollingInterval.Day,
                    outputTemplate:
                    "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level}] {Message:lj}{NewLine}{Exception}{NewLine}")
                .CreateLogger();
        }

        public void LogInformation(string message)
        {
            try
            {
                Log.Information(message);
            }
            catch (Exception e)
            {
                // ignored
            }
        }

        public void LogError(string message, Exception exception)
        {
            try
            {
                Log.Error(exception, message);
            }
            catch (Exception e)
            {
                // ignored
            }
        }

        public void LogFatal(string message, Exception exception)
        {
            try
            {
                Log.Fatal(exception, message);
            }
            catch (Exception e)
            {
                // ignored
            }
        }

        public void LogDebug(string message)
        {
            try
            {
                Log.Debug(message);
            }
            catch (Exception e)
            {
                // ignored
            }
        }

        public void LogVerbose(string message)
        {
            try
            {
                Log.Verbose(message);
            }
            catch (Exception e)
            {
                // ignored
            }
        }
    }
}