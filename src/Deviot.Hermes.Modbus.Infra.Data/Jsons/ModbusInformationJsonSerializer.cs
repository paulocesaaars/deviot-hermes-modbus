using Deviot.Hermes.Modbus.Domain.Entities;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Deviot.Hermes.Modbus.Infra.Data.Jsons
{
    public class ModbusInformationJsonSerializer : JsonConverter<ModbusInformationJson>
    {
        private ushort CheckDeviceType(Utf8JsonReader reader)
        {
            while (reader.Read())
            {
                if (reader.TokenType == JsonTokenType.PropertyName)
                {
                    if (reader.GetString().ToUpper() == "ModbusInformationTypeId".ToUpper())
                    {
                        reader.Read();
                        return reader.GetUInt16();
                    }
                }
            }

            return 0;
        }

        public override ModbusInformationJson Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var dataTypeId = CheckDeviceType(reader);

            if (ModbusInformationType.ModbusInformationDigital.Id == dataTypeId)
                return JsonSerializer.Deserialize<ModbusInformationDigitalJson>(ref reader, options);
            else if (ModbusInformationType.ModbusInformationAnalog.Id == dataTypeId)
                return JsonSerializer.Deserialize<ModbusInformationAnalogJson>(ref reader, options);
            else if (ModbusInformationType.ModbusInformationText.Id == dataTypeId)
                return JsonSerializer.Deserialize<ModbusInformationTextJson>(ref reader, options);
            else
                return JsonSerializer.Deserialize<ModbusInformationUndefinedJson>(ref reader, options);
        }

        public override void Write(Utf8JsonWriter writer, ModbusInformationJson value, JsonSerializerOptions options)
        {
            JsonSerializer.Serialize(writer, value as object, options);
        }
    }
}
