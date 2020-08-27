using Deviot.Hermes.Common;
using Deviot.Hermes.Modbus.Domain.Contracts;
using Deviot.Hermes.Modbus.Domain.Entities;
using Microsoft.Extensions.Logging;
using System;

namespace Deviot.Hermes.Mosquitto.Infra.CrossCutting.Driver
{
    public class MosquittoDriverService : IBrokerDriverService
    {
        #region Attributes
        private readonly ILogger<MosquittoDriverService> _logger;
        #endregion

        #region Properties
        #endregion

        #region Constants
        #endregion

        #region Constructors
        public MosquittoDriverService(ILogger<MosquittoDriverService> logger)
        {
            _logger = logger;
        }
        #endregion

        #region Methods

        #region Private
        #endregion

        #region Public
        public MosquittoBrokerStatus GetBrokerStatus()
        {
            throw new NotImplementedException();
        }

        public void UpdateDevice(MosquittoBroker brokerDevice, ModbusDevice modbusDevice)
        {
            throw new NotImplementedException();
        }

        public void SendData(string topic, DeviceData data)
        {
            throw new NotImplementedException();
        }

        public void SendData(string topic, ModbusDeviceStatus modbusDevice)
        {
            throw new NotImplementedException();
        }

        public void Start(MosquittoBroker brokerDevice, ModbusDevice modbusDevice)
        {
            throw new NotImplementedException();
        }

        public void Stop()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Protected
        #endregion

        #endregion

        #region Events
        public event ReceivedWriteDataHandler ReceivedWriteDataEvent;
        #endregion
    }
}
