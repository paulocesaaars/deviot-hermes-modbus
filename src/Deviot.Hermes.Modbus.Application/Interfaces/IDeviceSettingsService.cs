﻿using Deviot.Hermes.Modbus.Application.ModelViews;
using System.Threading.Tasks;

namespace Deviot.Hermes.Modbus.Application.Interfaces
{
    public interface IDeviceSettingsService
    {
        Task<ModbusDeviceViewModel> GetAsync();

        Task UpdateAsync(ModbusDeviceViewModel device);

        Task ResetAsync();
    }
}
