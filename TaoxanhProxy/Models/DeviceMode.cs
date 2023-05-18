namespace TaoxanhProxy.Models
{
    public class DeviceModel
    {
        public string? Id { get; set; }
        public Guid? UserId { get; set; }
        public string? Token { get; set; }
        public string? AndroidId { get; set; }
        public DeviceStatus? Status { get; set; }
        public DeviceSetting? Setting { get; set; }
        public Proxy? Proxy { get; set; }
        public string? Message  { get; set; } 
    }
    public enum DeviceStatus
    {
        Active, Pause, Approval, Delete
    }

    public class DeviceSetting
    {
        public bool? DebugMode { get; set; }
        public long? Time { get; set; }
        public string[]? AppRunning { get; set; }
    }
    public class Proxy
    {
        public string Id { get; set; }
        public string? Token { get; set; }
        public Guid? UserId { get; set; }
        public string? ProxyProfile { get; set; }
        public string? ProxyType { get; set; }
        public string? Ip { get; set; }
        public string? Username { get; set; }
        public string? Pasword { get; set; }
        public string? Port { get; set; }
        public string? Note { get; set; }
    }
}
