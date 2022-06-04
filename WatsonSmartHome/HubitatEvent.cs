namespace WatsonSmartHome
{
    public class HubitatEvent
    {
        public string Name { get; set; }
        public string Value { get; set; }
        public string DisplayName { get; set; }
        public string DeviceId { get; set; }
        public string DescriptionText { get; set; }
        public string Unit { get; set; }
        public string Type { get; set; }
        public string Data { get; set; }
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