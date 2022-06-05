using System;

namespace WatsonSmartHome.Devices
{
    public abstract record Device : IDevice
    {
        public string Name { get; init; }
        public uint Id { get; init; }
        public string Value { get; init; }

        protected Device(string id, string name, string value)
        {
            Id = Convert.ToUInt32(id);
            Name = name;
            Value = value;
        }
    }
}