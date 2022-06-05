namespace WatsonSmartHome.Devices
{
    public record ContactDevice : Device
    {
        public ContactDevice(string id, string name, string value) : base(id, name, value)
        {
        }
    }
}