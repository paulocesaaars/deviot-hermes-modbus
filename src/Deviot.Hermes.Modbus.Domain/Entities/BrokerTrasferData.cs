using System;
using System.Security.Cryptography;

namespace Deviot.Hermes.Modbus.Domain.Entities
{
    public class BrokerTrasferData
    {
        public string IdInformation { get; private set; }

        public string Description { get; private set; }

        public DateTime DateTime { get; private set; }

        public object Value { get; private set; }

        public bool Quality { get; private set; }

        public string Topic { get; private set; }

        public byte QosLevel { get; private set; }

        public bool Retain { get; private set; }

        public BrokerTrasferData(string idInformation, string description, object value, bool quality, string topic, byte qosLevel, bool retain)
        {
            IdInformation = idInformation;
            Description = description;
            DateTime = DateTime.Now;
            Value = value;
            Quality = quality;
            Topic = topic;
            QosLevel = qosLevel;
            Retain = retain;
        }
    }
}
