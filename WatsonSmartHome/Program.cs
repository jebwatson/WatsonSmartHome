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

        private static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureServices((_, services) =>
                {
                    services.AddSingleton<ILoggingService, SerilogService>();
                    services.AddSingleton<IDeviceFactory, DeviceFactory>();
                    services.AddMediatR(Assembly.GetExecutingAssembly());
                    services.AddSingleton<IHostedService, ConsoleApp>();
                });
        }
    }
}