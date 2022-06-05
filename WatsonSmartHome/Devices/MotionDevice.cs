namespace WatsonSmartHome.Devices
{
    public record MotionDevice : Device
    {
        public MotionDevice(string id, string name, string value) : base(id, name, value)
        {
        }
    }
}