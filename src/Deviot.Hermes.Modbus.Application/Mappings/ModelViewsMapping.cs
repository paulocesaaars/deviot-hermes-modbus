using AutoMapper;
using Deviot.Hermes.Modbus.Application.ModelViews;
using Deviot.Hermes.Modbus.Domain.Entities;

namespace Deviot.Hermes.Modbus.Application.Mappings
{
    public class ModelViewsMapping : Profile
    {
        public ModelViewsMapping()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                CreateMap<BrokerDeviceModelView, BrokerDevice>().ReverseMap();

                CreateMap<ModbusDeviceModelView, ModbusDevice>().ReverseMap();

                CreateMap<ModbusInformationModelView, ModbusInformation>()
                .Include<ModbusInformationDigitalModelView, ModbusInformationDigital>()
                .Include<ModbusInformationAnalogModelView, ModbusInformationAnalog>()
                .Include<ModbusInformationTextModelView, ModbusInformationText>()
                .Include<ModbusInformationUndefinedModelView, ModbusInformationUndefined>()
                .ReverseMap();

                CreateMap<ModbusInformationDigitalModelView, ModbusInformationDigital>().ReverseMap();
                CreateMap<ModbusInformationAnalogModelView, ModbusInformationAnalog>().ReverseMap();
                CreateMap<ModbusInformationTextModelView, ModbusInformationText>().ReverseMap();
                CreateMap<ModbusInformationUndefinedModelView, ModbusInformationUndefined>().ReverseMap();
            });
        }
    }
}