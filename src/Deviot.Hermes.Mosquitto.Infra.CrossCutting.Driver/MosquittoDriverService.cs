using Deviot.Hermes.Modbus.Domain.Contracts;
using Deviot.Hermes.Modbus.Domain.Entities;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace Deviot.Hermes.Mosquitto.Infra.CrossCutting.Driver
{
    public class MosquittoDriverService : IBrokerDriverService
    {
        #region Attributes
        private bool _status = false;
        private bool _statusConnection = false;
        private Thread _manageConnection;
        private MqttClient _mqttClient;
        private MqttBroker _mqttSettings;

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
        private void ManageComunication()
        {
            while (_status)
            {
                try
                {
                    if(_mqttClient != null)
                    {
                        if (!_mqttClient.IsConnected && _mqttSettings.Active)
                        {
                            _mqttClient.Connect(Guid.NewGuid().ToString());
                            _statusConnection = true;
                            if (_mqttSettings.Topics.Count() > 0)
                            {
                                var subscrabeTopics = _mqttSettings.Topics.Select(t => t.Topic).ToArray();
                                var subscrabeQos = _mqttSettings.Topics.Select(q => ConvertQosLevel(q.QosLevel)).ToArray();
                                var msgId = _mqttClient.Subscribe(subscrabeTopics, subscrabeQos);
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    _statusConnection = false;
                    _logger.LogError(e.Message);
                }

                Thread.Sleep(5000);
            }
        }

        private void MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
        {
            var value = Encoding.UTF8.GetString(e.Message);
            if (_mqttSettings.Topics.Any(t => t.Topic == e.Topic))
            {
                var topic = _mqttSettings.Topics.First(t => t.Topic == e.Topic);
                var deviceTransferData = new DeviceTransferData(topic.IdInformation, value);
                SendTransferData(deviceTransferData);
            }
        }

        private void SendTransferData(DeviceTransferData deviceTransferData)
        {
            try
            {
                if (ReceiveTransferDataFromDeviceEvent != null)
                    ReceiveTransferDataFromDeviceEvent(this, deviceTransferData);
            }
            catch(Exception exception)
            {
                _logger.LogError(exception.Message);
            }
        }

        private byte ConvertQosLevel(byte qosLevel)
        {
            if (int.TryParse(qosLevel.ToString(), out var qos))
                return qos > 2 ? byte.Parse("2") : byte.Parse(qos.ToString());

            return byte.Parse("0");
        }
        #endregion

        #region Public
        public MqttBrokerStatus GetBrokerStatus()
        {
            return new MqttBrokerStatus
                ( _mqttSettings.Description
                , _mqttSettings.Active
                , _mqttSettings.Ip
                , _mqttSettings.Port
                , _mqttSettings.Timeout
                , _statusConnection
                );
        }

        public void UpdateDevice(MqttBroker mqttBroker)
        {
            Stop();
            Start(mqttBroker);
        }

        public void SendData(BrokerTrasferData brokerTrasferData)
        {
            try
            {
                if(_mqttClient != null)
                {
                    if (_mqttClient.IsConnected)
                    {
                        var data = new DeviceData(brokerTrasferData.IdInformation, brokerTrasferData.IdInformation, brokerTrasferData.Value, brokerTrasferData.Quality);
                        var value = JsonSerializer.Serialize(data);
                        _mqttClient.Publish(brokerTrasferData.Topic, Encoding.UTF8.GetBytes(value), ConvertQosLevel(brokerTrasferData.QosLevel), brokerTrasferData.Retain);
                    }
                }
            }
            catch(Exception exception)
            {
                _logger.LogError(exception.Message);
            }
        }

        public void Start(MqttBroker mqttBroker)
        {
            if(!_status)
            {
                _mqttSettings = mqttBroker;
                if(_mqttSettings.Active)
                {
                    _mqttClient = new MqttClient(_mqttSettings.Ip);
                    _mqttClient.MqttMsgPublishReceived += MqttMsgPublishReceived;

                    _status = true;
                    _manageConnection = new Thread(ManageComunication);
                    _manageConnection.Start();
                }
            }
        }

        public void Stop()
        {
            _status = false;
            _statusConnection = false;

            if (_mqttClient != null)
                if(_mqttClient.IsConnected)
                    _mqttClient.Disconnect();
        }

        public void Dispose()
        {
            Stop();
            GC.SuppressFinalize(true);
        }
        #endregion

        #region Protected
        #endregion

        #endregion

        #region Events
        public event ReceiveTransferDataFromDeviceHandler ReceiveTransferDataFromDeviceEvent;
        #endregion
    }
}
