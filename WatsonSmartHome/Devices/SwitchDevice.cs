namespace WatsonSmartHome.Devices
{
    public record SwitchDevice : Device
    {
        public SwitchDevice(string id, string name, string value) : base(id, name, value)
        {
        }
    }
}