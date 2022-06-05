namespace WatsonSmartHome.Devices
{
    public interface IDevice
    {
        string Name { get; }
        uint Id { get; }
        string Value { get; }
    }
}