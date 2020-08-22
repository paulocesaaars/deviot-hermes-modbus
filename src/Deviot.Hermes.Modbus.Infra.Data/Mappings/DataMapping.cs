using AutoMapper;
using Deviot.Hermes.Modbus.Domain.Entities;
using Deviot.Hermes.Modbus.Infra.Data.Jsons;

namespace Deviot.Hermes.Modbus.Application.Mappings
{
    public class DataMapping : Profile
    {
        public DataMapping()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                CreateMap<ModbusDeviceJson, ModbusDevice>().ReverseMap();

                CreateMap<ModbusInformationJson, ModbusInformation>()
                .Include<ModbusInformationDigitalJson, ModbusInformationDigital>()
                .Include<ModbusInformationAnalogJson, ModbusInformationAnalog>()
                .Include<ModbusInformationTextJson, ModbusInformationText>()
                .Include<ModbusInformationUndefinedJson, ModbusInformationUndefined>()
                .ReverseMap();

                CreateMap<ModbusInformationDigitalJson, ModbusInformationDigital>().ReverseMap();
                CreateMap<ModbusInformationAnalogJson, ModbusInformationAnalog>().ReverseMap();
                CreateMap<ModbusInformationTextJson, ModbusInformationText>().ReverseMap();
                CreateMap<ModbusInformationUndefinedJson, ModbusInformationUndefined>().ReverseMap();
            });
        }
    }
}