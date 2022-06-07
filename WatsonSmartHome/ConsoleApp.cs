using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using WatsonSmartHome.Logging;

namespace WatsonSmartHome
{
    public class ConsoleApp : IHostedService
    {
        private readonly ILoggingService _loggingService;
        private readonly IConfiguration _configuration;
        private readonly IMediator _mediator;

        public ConsoleApp(IMediator mediator, ILoggingService loggingService, IConfiguration configuration)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _loggingService = loggingService ?? throw new ArgumentNullException(nameof(loggingService));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
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
            var hubitatConnection = _configuration.GetConnectionString("hubitat");

            if (string.IsNullOrEmpty(hubitatConnection))
            {
                _loggingService.LogInformation("Could not retrieve hubitat connection string");
                return;
            }
            
            var listener = new HttpListener();
            await new HubitatServer(_mediator, _loggingService)
                .Configure(listener, hubitatConnection, ProcessResponse)
                .Start();
        }

        private static byte[] ProcessResponse(string content)
        {
            //Console.WriteLine(content);
            return Array.Empty<byte>();
        }
    }
}