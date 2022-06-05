namespace WatsonSmartHome.Devices
{
    public interface IDeviceFactory
    {
        IDevice Create(HubitatEvent hubitatEvent);
    }
}