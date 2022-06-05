namespace WatsonSmartHome.Devices
{
    public record EmptyDevice : Device
    {
        public EmptyDevice(string id, string name, string value) : base(id, name, value)
        {
        }
    }
}