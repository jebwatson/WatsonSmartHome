using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Hosting;
using WatsonSmartHome.Logging;

namespace WatsonSmartHome
{
    public class ConsoleApp : IHostedService
    {
        private readonly ILoggingService _loggingService;
        private readonly IMediator _mediator;

        public ConsoleApp(IMediator mediator, ILoggingService loggingService)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _loggingService = loggingService ?? throw new ArgumentNullException(nameof(loggingService));
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            _loggingService.LogInformation("Starting Console Application");

            await StartServer();
            _loggingService.LogInformation("Press any key to stop...");
            Console.ReadKey();
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        private async Task StartServer()
        {
            var listener = new HttpListener();
            await new HubitatServer(_mediator, _loggingService)
                .Configure(listener, "http://192.168.1.220:8567/", ProcessResponse)
                .Start();
        }

        private static byte[] ProcessResponse(string content)
        {
            //Console.WriteLine(content);
            return Array.Empty<byte>();
        }
    }
}