using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using WatsonSmartHome.Devices;

namespace WatsonSmartHome.Messaging
{
    public class CreateDeviceCommand : IRequest<IDevice>
    {
        public HubitatEvent HubitatEvent { get; init; }
        
        public CreateDeviceCommand(HubitatEvent hubitatEvent)
        {
            HubitatEvent = hubitatEvent ?? throw new ArgumentNullException(nameof(hubitatEvent));
        }
    }
    
    public class CreateDeviceHandler : IRequestHandler<CreateDeviceCommand, IDevice>
    {
        private readonly IDeviceFactory _deviceFactory;

        public CreateDeviceHandler(IDeviceFactory deviceFactory)
        {
            _deviceFactory = deviceFactory ?? throw new ArgumentNullException(nameof(deviceFactory));
        }

        public Task<IDevice> Handle(CreateDeviceCommand request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_deviceFactory.Create(request.HubitatEvent));
        }
    }
}