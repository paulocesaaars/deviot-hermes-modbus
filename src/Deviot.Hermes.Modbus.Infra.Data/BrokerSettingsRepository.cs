using AutoMapper;
using Deviot.Hermes.Modbus.Domain.Contracts;
using Deviot.Hermes.Modbus.Domain.Entities;
using Deviot.Hermes.Modbus.Infra.Data.Jsons;
using System;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace Deviot.Hermes.Modbus.Infra.Data
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
        private async Task<MosquittoBroker> ReadFileAsync()
        {
            try
            {
                if (!File.Exists(PATH_JSON))
                    await ResetAsync();

                using (FileStream stream = File.OpenRead(PATH_JSON))
                {
                    var broker = await JsonSerializer.DeserializeAsync<MosquittoBrokerJson>(stream);
                    return _mapper.Map<MosquittoBroker>(broker);
                }
            }
            catch(Exception)
            {
                throw new InvalidDataException("Houve um problema ao ler o arquivo de configurações, dados corrompidos.");
            }
        }

        private async Task WriteFileAsync(MosquittoBroker broker)
        {
            try
            {
                var brokerModelView = _mapper.Map<MosquittoBrokerJson>(broker);

                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                    WriteIndented = true,
                    Encoder = System.Text.Encodings.Web.JavaScriptEncoder.Create(System.Text.Unicode.UnicodeRanges.All)
                };

                var json = JsonSerializer.Serialize(brokerModelView, options);

                await File.WriteAllTextAsync(PATH_JSON, json);
            }
            catch (Exception)
            {
                throw new InvalidDataException("Houve um problema ao escrever no arquivo de configurações, dados corrompidos.");
            }
        }
        #endregion

        #region Public
        public async Task<MosquittoBroker> GetAsync()
        {
            return await ReadFileAsync();
        }

        public async Task UpdateAsync(MosquittoBroker broker)
        {
            await WriteFileAsync(broker);
        }

        public async Task<MosquittoBroker> ResetAsync()
        {
            var broker = new MosquittoBroker("Broker Mosquitto", false, "127.0.0.1", 502, 1000);
            await WriteFileAsync(broker);

            return broker;
        }
        #endregion

        #region Protected
        #endregion

        #endregion

        #region Events
        #endregion
    }
}
