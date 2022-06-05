namespace WatsonSmartHome
{
    public class HubitatEvent
    {
        public string Name { get; set; } = null!;
        public string Value { get; set; } = null!;
        public string DisplayName { get; set; } = null!;
        public string DeviceId { get; set; } = null!;
        public string DescriptionText { get; set; } = null!;
        public string Unit { get; set; } = null!;
        public string Type { get; set; } = null!;
        public string Data { get; set; } = null!;
    }
    
    //{"content":
    //{"name":"motion",
    //"value":"inactive",
    //"displayName":"Guest Bathroom Motion",
    //"deviceId":"137",
    //"descriptionText":"Guest Bathroom Motion is inactive",
    //"unit":null,
    //"type":null,
    //"data":null}}
}