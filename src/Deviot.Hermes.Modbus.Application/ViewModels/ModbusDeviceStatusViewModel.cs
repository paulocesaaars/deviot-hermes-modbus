namespace Deviot.Hermes.Modbus.Application.ModelViews
{
    public class ModbusDeviceStatusViewModel
    {
        public string Description { get; set; }

        public bool Active { get; set; }

        public string Ip { get; set; }

        public int Port { get; set; }

        public int Scan { get; set; }

        public int Timeout { get; set; }

        public bool Status { get; set; }
    }
}
