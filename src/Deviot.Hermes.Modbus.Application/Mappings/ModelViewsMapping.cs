using AutoMapper;
using Deviot.Hermes.Common;
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
                CreateMap<DeviceDataViewModel, DeviceData>().ReverseMap();

                CreateMap<ModbusDeviceViewModel, ModbusDevice>().ReverseMap();
                CreateMap<ModbusDeviceStatusViewModel, ModbusDeviceStatus>().ReverseMap();                

                CreateMap<ModbusInformationViewModel, ModbusInformation>()
                .Include<ModbusInformationDigitalViewModel, ModbusInformationDigital>()
                .Include<ModbusInformationAnalogViewModel, ModbusInformationAnalog>()
                .Include<ModbusInformationTextViewModel, ModbusInformationText>()
                .Include<ModbusInformationUndefinedViewModel, ModbusInformationUndefined>()
                .ReverseMap();

                CreateMap<ModbusInformationDigitalViewModel, ModbusInformationDigital>().ReverseMap();
                CreateMap<ModbusInformationAnalogViewModel, ModbusInformationAnalog>().ReverseMap();
                CreateMap<ModbusInformationTextViewModel, ModbusInformationText>().ReverseMap();
                CreateMap<ModbusInformationUndefinedViewModel, ModbusInformationUndefined>().ReverseMap();

                CreateMap<MosquittoBrokerViewModel, MosquittoBroker>().ReverseMap();
            });
        }
    }
}