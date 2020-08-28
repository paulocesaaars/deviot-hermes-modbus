using Deviot.Hermes.Common;
using Deviot.Hermes.Modbus.Domain.Contracts;
using Deviot.Hermes.Modbus.Domain.Entities;
using Microsoft.Extensions.Logging;
using Modbus.Device;
using Modbus.Utility;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Deviot.Hermes.Modbus.Infra.CrossCutting.Driver
{
    public class ModbusDriverService : IDeviceDriverService
    {
        #region Attributes
        private bool _status = false;
        private bool _statusConnection = false;
        private bool _writing = false;
        private Thread _manageConnection;
        private Dictionary<string, DeviceData> _data;
        private Dictionary<string, int> _numberOfReadingAttempts;
        private Domain.Entities.ModbusDevice _modbusSettings;

        private readonly ILogger<ModbusDriverService> _logger;
        #endregion

        #region Properties
        #endregion

        #region Constants
        #endregion

        #region Constructors
        public ModbusDriverService(ILogger<ModbusDriverService> logger)
        {
            _logger = logger;
            _data = new Dictionary<string, DeviceData>();
            _numberOfReadingAttempts = new Dictionary<string, int>();
        }
        #endregion

        #region Methods

        #region Private
        private bool ConvertObjectForBoolean(object value)
        {
            if (Utility.CheckObjectNullOrEmpty(value))
                return false;

            if (value.ToString() == "0" || value.ToString() == "false")
                return false;

            return true;
        }

        private ushort[] GetUShortArray(double value, bool swap = false)
        {
            var b1 = new BitArray(BitConverter.GetBytes(value));
            var bitValue1 = new StringBuilder();
            var bitValue2 = new StringBuilder();

            for (int x = 31; x >= 16; x--)
                bitValue1.Append(Convert.ToInt16(b1[x]).ToString());

            for (int x = 15; x >= 0; x--)
                bitValue2.Append(Convert.ToInt16(b1[x]).ToString());

            var result = new ushort[2];

            if (swap)
            {
                result[0] = Convert.ToUInt16(bitValue1.ToString(), 2);
                result[1] = Convert.ToUInt16(bitValue2.ToString(), 2);
            }
            else
            {
                result[0] = Convert.ToUInt16(bitValue2.ToString(), 2);
                result[1] = Convert.ToUInt16(bitValue1.ToString(), 2);
            }

            return result;
        }

        private ushort[] GetUShortArray(char[] text, int lengthLimit)
        {
            var result = new ushort[lengthLimit];
            var forLimit = text.Length > lengthLimit ? lengthLimit : text.Length;

            for (int x = 0; x < forLimit; x++)
                result[x] = (ushort)text[x];

            return result;
        }

        private void WriteSingleCoil(byte id, ushort address, bool value)
        {
            if(_status)
            {
                using (var tcpClient = new TcpClient())
                {
                    tcpClient.ReceiveTimeout = _modbusSettings.Timeout;
                    tcpClient.SendTimeout = _modbusSettings.Timeout;

                    var modbusIpMaster = ModbusIpMaster.CreateIp(tcpClient);
                    tcpClient.Connect(_modbusSettings.Ip, _modbusSettings.Port);
                    modbusIpMaster.WriteSingleCoil(id, address, value);

                    tcpClient.Close();
                }
            }
        }

        private void WriteSingleRegister(byte id, ushort address, ushort value)
        {
            if(_status)
            {
                using (var tcpClient = new TcpClient())
                {
                    tcpClient.ReceiveTimeout = _modbusSettings.Timeout;
                    tcpClient.SendTimeout = _modbusSettings.Timeout;

                    var modbusIpMaster = ModbusIpMaster.CreateIp(tcpClient);
                    tcpClient.Connect(_modbusSettings.Ip, _modbusSettings.Port);
                    modbusIpMaster.WriteSingleRegister(id, address, value);

                    tcpClient.Close();
                }
            }
        }

        private bool ValidateData(ModbusInformation information, string data)
        {
            if (information.ModbusInformationType.Equals(ModbusInformationType.ModbusInformationDigital))
            {
                if (!bool.TryParse(data, out var result))
                    throw new InvalidOperationException("O valor não é do tipo digital");
            }
            else if (information.ModbusInformationType.Equals(ModbusInformationType.ModbusInformationAnalog))
            {
                if (!double.TryParse(data, out var result))
                    throw new InvalidOperationException("O valor não é do tipo decimal");
            }

            return true;
        }

        private void SendModbusData(ModbusInformation modbusInformation, string data)
        {
            if (modbusInformation.ModbusFunctionType.Equals(ModbusFunctionType.CoilStatus))
            {
                WriteSingleCoil(modbusInformation.DeviceId, modbusInformation.StartAddress, ConvertObjectForBoolean(data));
            }
            else if (modbusInformation.ModbusFunctionType.Equals(ModbusFunctionType.HoldingRegister))
            {
                if (modbusInformation.ModbusInformationType.Equals(ModbusInformationType.ModbusInformationDigital))
                {
                    var value = ConvertObjectForBoolean(data);
                    WriteSingleRegister(modbusInformation.DeviceId, modbusInformation.StartAddress, value ? '1' : '0');
                }
                else if (modbusInformation.ModbusInformationType.Equals(ModbusInformationType.ModbusInformationAnalog))
                {
                    var modbusInformationDecimal = (ModbusInformationAnalog)modbusInformation;
                    var multiplicador = (modbusInformationDecimal.MaxRawValue - modbusInformationDecimal.MinRawValue) / (modbusInformationDecimal.MaxValue - modbusInformationDecimal.MinValue);

                    if (modbusInformationDecimal.NumberOfAddresses == 1)
                    {
                        UInt16 registerValue;
                        if (modbusInformationDecimal.Signed)
                        {
                            var value = Convert.ToDouble(data) * multiplicador;

                            if (value < 0)
                                registerValue = Convert.ToUInt16(value + 65536);
                            else
                                registerValue = Convert.ToUInt16(value);
                        }
                        else
                        {
                            registerValue = Convert.ToUInt16(Convert.ToDouble(data) * multiplicador);
                        }

                        WriteSingleRegister(modbusInformationDecimal.DeviceId, modbusInformationDecimal.StartAddress, registerValue);
                    }
                    else if (modbusInformationDecimal.NumberOfAddresses == 2)
                    {
                        ushort[] registers;
                        UInt32 value;

                        if (modbusInformationDecimal.Signed)
                        {
                            var valueAux = Convert.ToDouble(data) * multiplicador;

                            if (valueAux < 0)
                                value = Convert.ToUInt32(valueAux + 4294967295);
                            else
                                value = Convert.ToUInt32(valueAux);
                        }
                        else
                        {
                            value = Convert.ToUInt32(Convert.ToDouble(data) * multiplicador);
                        }

                        registers = GetUShortArray(value, modbusInformationDecimal.Swap);
                        var address = modbusInformationDecimal.StartAddress;

                        foreach (var register in registers)
                        {
                            WriteSingleRegister(modbusInformationDecimal.DeviceId, address, register);
                            address++;
                        }
                    }
                }
                else if (modbusInformation.ModbusInformationType.Equals(ModbusInformationType.ModbusInformationText))
                {
                    var registers = GetUShortArray(data.ToString().ToCharArray(), modbusInformation.NumberOfAddresses);

                    var address = modbusInformation.StartAddress;
                    foreach (var register in registers)
                    {
                        WriteSingleRegister(modbusInformation.DeviceId, address, register);
                        address++;
                    }
                }
            }
            else
            {
                throw new InvalidOperationException($"Não foi possível escrever no registro Modbus, função modbus inválida.");
            }
        }

        private void SendTransferData(BrokerTrasferData brokerTrasferData)
        {
            try
            {
                if (ReceiveTransferDataFromBrokerEvent != null)
                    ReceiveTransferDataFromBrokerEvent(this, brokerTrasferData);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception.Message);
            }
        }

        private BrokerTrasferData ConvertDeviceDataForBrokerTransferData(DeviceData data)
        {
            var information = _modbusSettings.ModbusInformations.First(i => i.Id == data.IdInformation);
            return new BrokerTrasferData(data.IdInformation, data.Description, data.Value, data.Quality, information.Topic, information.QosLevel, information.Retain);
        }

        private void SetData(string idInformation, object value)
        {
            _data[idInformation].SetValue(value);
            SendTransferData(ConvertDeviceDataForBrokerTransferData(_data[idInformation]));
        }

        private void SetBadData(string idInformation)
        {
            _data[idInformation].BadValue();
            SendTransferData(ConvertDeviceDataForBrokerTransferData(_data[idInformation]));
        }

        private void SetBadAllData()
        {
            foreach (var data in _data)
            {
                data.Value.BadValue();
                SendTransferData(ConvertDeviceDataForBrokerTransferData(data.Value));
            }
        }

        private void SetDigitalData(ModbusInformation modbusInformation, bool[] registers)
        {
            SetData(modbusInformation.Id, registers[0]);
        }

        private void SetShortData(ModbusInformation modbusInformation, ushort[] registers)
        {
            if (modbusInformation.ModbusInformationType.Equals(ModbusInformationType.ModbusInformationDigital))
            {
                var value = ConvertObjectForBoolean(registers[0]);
                SetData(modbusInformation.Id, value);
            }
            else if (modbusInformation.ModbusInformationType.Equals(ModbusInformationType.ModbusInformationAnalog))
            {
                Double value;
                var modbusInformationDecimal = (ModbusInformationAnalog)modbusInformation;
                if (modbusInformationDecimal.NumberOfAddresses == 1)
                {
                    value = registers[0];
                    if (modbusInformationDecimal.Signed && value > 32768)
                        value -= 65536;
                }
                else
                {
                    if (modbusInformationDecimal.Swap)
                        value = ModbusUtility.GetUInt32(registers[1], registers[0]);
                    else
                        value = ModbusUtility.GetUInt32(registers[0], registers[1]);

                    if (modbusInformationDecimal.Signed && value > 2147483648)
                        value -= 4294967295;
                }

                if (value > modbusInformationDecimal.MaxRawValue)
                    value = modbusInformationDecimal.MaxRawValue;

                if (value < modbusInformationDecimal.MinRawValue)
                    value = modbusInformationDecimal.MinRawValue;

                var multiplicator = (modbusInformationDecimal.MaxValue - modbusInformationDecimal.MinValue) / (modbusInformationDecimal.MaxRawValue - modbusInformationDecimal.MinRawValue);
                value *= multiplicator;

                SetData(modbusInformationDecimal.Id, Math.Round(value, modbusInformationDecimal.DecimalPlaces));
            }
            else
            {
                var value = new StringBuilder();
                foreach (var register in registers)
                {
                    char caracter;
                    if (register == 0)
                        caracter = ' ';
                    else if (register > 255)
                        caracter = (char)255;
                    else
                        caracter = (char)register;

                    value.Append(caracter);
                }

                SetData(modbusInformation.Id, value.ToString());
            }
        }

        private void ReceiveModbusData()
        {
            try
            {
                if(_status)
                {
                    using (var tcpClient = new TcpClient())
                    {
                        tcpClient.ReceiveTimeout = _modbusSettings.Timeout;
                        tcpClient.SendTimeout = _modbusSettings.Timeout;

                        var modbusIpMaster = ModbusIpMaster.CreateIp(tcpClient);
                        tcpClient.Connect(_modbusSettings.Ip, _modbusSettings.Port);
                        _statusConnection = true;

                        foreach (var modbusInformation in _modbusSettings.ModbusInformations)
                        {
                            try
                            {
                                if (modbusInformation.ModbusFunctionType.Equals(ModbusFunctionType.CoilStatus))
                                    SetDigitalData(modbusInformation, modbusIpMaster.ReadCoils(modbusInformation.DeviceId, modbusInformation.StartAddress, 1));
                                else if (modbusInformation.ModbusFunctionType.Equals(ModbusFunctionType.InputRegister))
                                    SetDigitalData(modbusInformation, modbusIpMaster.ReadInputs(modbusInformation.DeviceId, modbusInformation.StartAddress, 1));
                                else if (modbusInformation.ModbusFunctionType.Equals(ModbusFunctionType.HoldingRegister))
                                    SetShortData(modbusInformation, modbusIpMaster.ReadHoldingRegisters(modbusInformation.DeviceId, modbusInformation.StartAddress, modbusInformation.NumberOfAddresses));
                                else if (modbusInformation.ModbusFunctionType.Equals(ModbusFunctionType.InputRegister))
                                    SetShortData(modbusInformation, modbusIpMaster.ReadInputRegisters(modbusInformation.DeviceId, modbusInformation.StartAddress, modbusInformation.NumberOfAddresses));

                                _numberOfReadingAttempts[modbusInformation.Id] = 0;
                            }
                            catch (Exception exception)
                            {
                                _logger.LogError(exception.Message);
                                _numberOfReadingAttempts[modbusInformation.Id]++;

                                if (_numberOfReadingAttempts[modbusInformation.Id] >= _modbusSettings.MaximumNumberOfReadingAttempts)
                                {
                                    _numberOfReadingAttempts[modbusInformation.Id] = 0;
                                    SetBadData(modbusInformation.Id);
                                }
                            }
                        }

                        tcpClient.Close();
                    }
                }
            }
            catch (Exception exception)
            {
                _logger.LogError(exception.Message);
                _statusConnection = false;
                SetBadAllData();
            }
        }

        private void ManageComunication()
        {
            while (_status)
            {
                var startTime = DateTime.Now;
                try
                {
                    if(!_writing)
                        ReceiveModbusData();
                }
                catch (Exception exception)
                {
                    _logger.LogError(exception.Message);
                }

                var timeDif = DateTime.Now - startTime;
                if (timeDif.TotalMilliseconds < _modbusSettings.Scan)
                    Thread.Sleep(_modbusSettings.Scan - Convert.ToInt32(timeDif.TotalMilliseconds));
            }
        }
        #endregion

        #region Public
        public ModbusDeviceStatus GetDeviceStatus()
        {
            return new ModbusDeviceStatus
                ( _modbusSettings.Description
                , _modbusSettings.Active
                , _modbusSettings.Ip
                , _modbusSettings.Port
                , _modbusSettings.Scan
                , _modbusSettings.Timeout
                , _statusConnection
                );
        }

        public IEnumerable<DeviceData> GetData() => _data.Select(d => d.Value);

        public IEnumerable<DeviceData> GetData(IEnumerable<string> idInformations)
        {
            return _data.Where(d => idInformations.Contains(d.Key)).Select(d => d.Value);
        }

        public void SendData(string idInformation, string data)
        {
            if(_modbusSettings.ModbusInformations.Any(d => d.Id == idInformation))
            {
                _writing = true;
                var information = _modbusSettings.ModbusInformations.First(d => d.Id == idInformation);
                try
                {
                    if (!information.EnableWrite)
                        throw new InvalidOperationException($"Não foi possível escrever, a informação é somente de leitura.");

                    if(ValidateData(information, data))
                        SendModbusData(information, data);
                }
                catch(InvalidOperationException exception)
                {
                    throw new InvalidOperationException($"Não foi possível escrever, dados inválidos.", exception);
                }
                catch(Exception)
                {
                    throw new InvalidOperationException($"Não foi possível escrever, dados inválidos.");
                }
            }
            else
            {
                throw new InvalidOperationException($"Não foi possível escrever, o id {idInformation} é inválido.");
            }
        }

        public void SendData(DeviceTransferData data)
        {
            try
            {
                SendData(data.IdInformation, data.Value);
            }
            catch(Exception exception)
            {
                _logger.LogError(exception.Message);
            }
        }

        public void UpdateDevice(Domain.Entities.ModbusDevice modbusDevice)
        {
            _status = false;
            foreach (var data in _data)
            {
                if (!modbusDevice.ModbusInformations.Any(m => m.Id == data.Key))
                {
                    _data.Remove(data.Key);
                    _numberOfReadingAttempts.Remove(data.Key);
                }
            }

            foreach (var information in modbusDevice.ModbusInformations)
            {
                if (!_data.Any(d => d.Key == information.Id))
                {
                    _data.Add(information.Id, new DeviceData(information.Id, information.Description, null, false));
                    _numberOfReadingAttempts.Add(information.Id, 0);
                }
            }

            if (modbusDevice.Active)
            {
                _status = true;
                _modbusSettings = modbusDevice;
                _manageConnection = new Thread(ManageComunication);
                _manageConnection.Start();
            }
            else
            {
                Stop();
                _modbusSettings = modbusDevice;
            }
        }

        public void Start(Domain.Entities.ModbusDevice modbusDevice)
        {
            if(!_status)
            {
                _data.Clear();
                foreach (var information in modbusDevice.ModbusInformations)
                    _data.Add(information.Id, new DeviceData(information.Id, information.Description, null, false));

                _numberOfReadingAttempts.Clear();
                foreach (var modbusInformation in modbusDevice.ModbusInformations)
                    _numberOfReadingAttempts.Add(modbusInformation.Id, 0);

                if(modbusDevice.Active)
                {
                    _status = true;
                    _modbusSettings = modbusDevice;
                    _manageConnection = new Thread(ManageComunication);
                    _manageConnection.Start();
                }
            }
        }

        public void Stop()
        {
            _status = false;
            SetBadAllData();
        }

        public void Dispose()
        {
            _status = false;
            _data.Clear();
            _numberOfReadingAttempts.Clear();

            GC.SuppressFinalize(true);
        }

        


        #endregion

        #region Protected
        #endregion

        #endregion

        #region Events
        public event ReceiveTransferDataFromBrokerHandler ReceiveTransferDataFromBrokerEvent;
        #endregion
    }
}
