using AutoMapper;
using Deviot.Hermes.Modbus.Common.Entities;
using Deviot.Hermes.Modbus.Common.ModelViews;
using Deviot.Hermes.Modbus.Core.Contracts;
using System;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace Deviot.Hermes.Modbus.Data
{
    public class BrokerSettingsRepository : IBrokerSettingsRepository
    {
        #region Attributes
        private readonly IMapper _mapper;
        #endregion

        #region Properties
        #endregion

        #region Constants
        private const string PATH_DIRECTORY = @".\Settings\";
        private const string PATH_JSON = PATH_DIRECTORY + "Broker.json";
        #endregion

        #region Constructors
        public BrokerSettingsRepository(IMapper mapper)
        {
            _mapper = mapper;

            if (!Directory.Exists(PATH_DIRECTORY))
                Directory.CreateDirectory(PATH_DIRECTORY);
        }
        #endregion

        #region Methods
        #region Private
        private async Task<BrokerDevice> ReadFileAsync()
        {
            try
            {
                if (!File.Exists(PATH_JSON))
                    await ResetAsync();

                using (FileStream stream = File.OpenRead(PATH_JSON))
                {
                    var device = await JsonSerializer.DeserializeAsync<BrokerDeviceModelView>(stream);
                    return _mapper.Map<BrokerDevice>(device);
                }
            }
            catch(Exception)
            {
                throw new InvalidDataException("Houve um problema ao ler o arquivo de configurações, dados corrompidos.");
            }
        }

        private async Task WriteFileAsync(BrokerDevice device)
        {
            try
            {
                var devicesViewModel = _mapper.Map<BrokerDeviceModelView>(device);

                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                    WriteIndented = true,
                    Encoder = System.Text.Encodings.Web.JavaScriptEncoder.Create(System.Text.Unicode.UnicodeRanges.All)
                };

                var json = JsonSerializer.Serialize(devicesViewModel, options);

                await File.WriteAllTextAsync(PATH_JSON, json);
            }
            catch (Exception)
            {
                throw new InvalidDataException("Houve um problema ao escrever no arquivo de configurações, dados corrompidos.");
            }
        }
        #endregion

        #region Public
        public async Task<BrokerDevice> GetAsync()
        {
            return await ReadFileAsync();
        }

        public async Task UpdateAsync(BrokerDevice device)
        {
            await WriteFileAsync(device);
        }

        public async Task<BrokerDevice> ResetAsync()
        {
            var device = new BrokerDevice("Broker MQTT", false, "127.0.0.1", 502);
            await WriteFileAsync(device);

            return device;
        }
        #endregion

        #region Protected
        #endregion

        #endregion

        #region Events
        #endregion
    }
}
