using System;
using System.IO;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Newtonsoft.Json.Linq;
using WatsonSmartHome.Devices;
using WatsonSmartHome.Logging;
using WatsonSmartHome.Messaging;
#pragma warning disable 4014

namespace WatsonSmartHome
{
    public delegate byte[] ProcessDataDelegate(string data);

    public class HubitatServer
    {
        private const int HandlerThread = 2;
        private readonly ILoggingService _loggingService;
        private readonly IMediator _mediator;
        private ProcessDataDelegate _handler = null!;
        private bool _isConfigured;
        private HttpListener _listener = null!;

        public HubitatServer(IMediator mediator, ILoggingService loggingService)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _loggingService = loggingService ?? throw new ArgumentNullException(nameof(loggingService));
        }

        public HubitatServer Configure(HttpListener listener, string url, ProcessDataDelegate handler)
        {
            _listener = listener;
            _handler = handler;
            listener.Prefixes.Add(url);
            _isConfigured = true;
            return this;
        }

        public Task<HubitatServer> Start()
        {
            if (!_isConfigured || _listener.IsListening) return Task.FromResult(this);

            _listener.Start();

            for (var i = 0; i < HandlerThread; i++)
            {
                _listener.GetContextAsync().ContinueWith(ProcessRequestHandler);
            }

            return Task.FromResult(this);
        }

        public void Stop()
        {
            if (_listener.IsListening) _listener.Stop();
        }

        private async Task ProcessRequestHandler(Task<HttpListenerContext> result)
        {
            _loggingService.LogInformation("Processing request");
            var context = result.Result;

            if (!_listener.IsListening) return;

            // Start new listener which replaces this
            _listener.GetContextAsync().ContinueWith(ProcessRequestHandler);

            // Read request
            string request = new StreamReader(context.Request.InputStream).ReadToEnd();

            JObject hubitatMessage = JObject.Parse(request);
            var hubitatEventToken = hubitatMessage["content"];

            var hubitatEvent = hubitatEventToken?.ToObject<HubitatEvent>();
            IDevice device;

            if (hubitatEvent is not null)
            {
                _loggingService.LogInformation("Hubitat event received");
                device = await _mediator.Send(new CreateDeviceCommand(hubitatEvent), CancellationToken.None);
                _loggingService.LogInformation($"Device {device.Name} received");
            }

            // Prepare response
            var responseBytes = _handler.Invoke(request);
            context.Response.ContentLength64 = responseBytes.Length;

            var output = context.Response.OutputStream;
            output.WriteAsync(responseBytes, 0, responseBytes.Length);
            output.Close();
            
            _loggingService.LogInformation("Response sent");
        }
    }
}