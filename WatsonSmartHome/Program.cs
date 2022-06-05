using System;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WatsonSmartHome.Devices;
using WatsonSmartHome.Logging;

namespace WatsonSmartHome
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            var hostBuilder = CreateHostBuilder(args);
            await hostBuilder.RunConsoleAsync();
        }

        private static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostingContext, services) =>
                {
                    services.AddSingleton<ILoggingService, SerilogService>();
                    services.AddSingleton<IDeviceFactory, DeviceFactory>();
                    services.AddMediatR(Assembly.GetExecutingAssembly());
                    services.AddSingleton<IHostedService, ConsoleApp>();
                });
    }
}