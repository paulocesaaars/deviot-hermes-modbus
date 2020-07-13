using Deviot.Hermes.Common.Entities;
using Deviot.Hermes.Modbus.Device.Core.Contracts;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Deviot.Hermes.Modbus.Device.Core.Services
{
    public class DeviceManagerService : IDeviceManagerService
    {
        #region Attributes
        private bool _status = false;

        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<DeviceManagerService> _logger;
        private readonly Dictionary<string, DeviceData> _data;
        private readonly Dictionary<string, DeviceDriverService> _device;
        #endregion

        #region Properties
        #endregion

        #region Constants
        #endregion

        #region Constructors
        public DeviceManagerService(ILogger<DeviceManagerService> logger, IServiceProvider serviceProvider)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;

            _data = new Dictionary<string, DeviceData>();
            _devicesDrivers = new Dictionary<string, DeviceDriverService>();
        }
        #endregion

        #region Methods

        #region Private
        private void Clear()
        {
            _data.Clear();
            _devicesDrivers.Clear();
        }
        #endregion

        #region Public
        public IEnumerable<DeviceData> GetData()
        {
            if (_data.Any(d => d.Key == idInformation))
                return _data.First(d => d.Key == idInformation).Value;
            else
                throw new InvalidOperationException($"Não existe um dado com esse id {idInformation}.");
        }

        public IEnumerable<DeviceData> GetData(IEnumerable<string> idInformations)
        {
            var result = new List<InformationData>(idInformations.Count());
            foreach (var id in idInformations)
                result.Add(GetData(id));

            return result;
        }
                
        public void UpdateDevice(Common.Entities.Device device)
        {
            
        }
        
        public void SendData(string idInformation, object value)
        {
            if (_devicesInformations.Any(d => d.Key == idInformation))
            {
                var idDevice = _devicesInformations[idInformation];
                _devicesDrivers[idDevice].SendInformationData(idInformation, value);
            }
            else
            {
                throw new InvalidOperationException($"Não foi encontrado nenhuma informação com o id {idInformation}.");
            }
        }

        public async Task Start()
        {
            if (!_status)
            {
                Stop();
                Clear();
                _status = true;
                var deviceDriverServices = await GetDriverDevices();

                foreach (var deviceDriverService in deviceDriverServices)
                    AddDeviceDriver(deviceDriverService);
            }
        }

        public void Stop()
        {
            _status = false;
            Parallel.ForEach(_devicesDrivers, device =>
            {
                device.Value.Stop();
            });
        }

        public void Dispose()
        {
            Stop();
            Clear();
            GC.SuppressFinalize(true);
        }
        #endregion

        #region Protected
        #endregion

        #endregion

        #region Events
        #endregion
    }
}
