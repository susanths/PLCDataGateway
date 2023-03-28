namespace ModbusAPI.Models
{
    public class Device
    {
        public string IpAddress { get; set; } = "127.0.0.1";

        public int DeviceId { get; set; }

        public int Port { get; set; }

    }
}
