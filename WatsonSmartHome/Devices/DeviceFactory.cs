#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using WatsonSmartHome.Logging;

namespace WatsonSmartHome.Devices
{
    public class DeviceFactory : IDeviceFactory
    {
        private readonly IDictionary<string, Type> _deviceTypes = new Dictionary<string, Type>();
        private readonly ILoggingService _loggingService;

        public DeviceFactory(ILoggingService loggingService)
        {
            _loggingService = loggingService ?? throw new ArgumentNullException(nameof(loggingService));

            var deviceTypes = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(a => a.GetTypes())
                .Where(t => typeof(IDevice).IsAssignableFrom(t) && t != typeof(Device))
                .ToArray();

            foreach (var deviceType in deviceTypes)
            {
                var deviceName = deviceType.Name.Replace("Device", "").ToLower();
                _deviceTypes.Add(deviceName, deviceType);
            }
        }

        public IDevice Create(HubitatEvent hubitatEvent)
        {
            // TODO: wrap this in a task for performance
            if (!_deviceTypes.ContainsKey(hubitatEvent.Name))
            {
                _loggingService.LogInformation($"Received a hub message with unknown type, {hubitatEvent.Name}");
                return new EmptyDevice(hubitatEvent.DeviceId, hubitatEvent.DisplayName, hubitatEvent.Value);
            }

            _loggingService.LogInformation($"Creating device of type {hubitatEvent.Name}");
            var device = Activator.CreateInstance(
                _deviceTypes[hubitatEvent.Name],
                hubitatEvent.DeviceId,
                hubitatEvent.DisplayName,
                hubitatEvent.Value)! as IDevice;

            if (device is null)
            {
                _loggingService.LogInformation($"Could not create device of type {hubitatEvent.Name}");
                return new EmptyDevice(hubitatEvent.DeviceId, hubitatEvent.DisplayName, hubitatEvent.Value);
            }

            return device;
        }
    }
}