using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Deviot.Hermes.Modbus.Service.Api
{
    public static class DatabaseConfig
    {
        public static void AddDatabaseConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            //// Banco de dados RulesEngine
            //services.Configure<RulesEngineSettings>(
            //    configuration.GetSection(nameof(RulesEngineSettings)));

            //// Banco de dados KeepTrue.RulesEngineDatabase.Sdk
            //services.Configure<RulesEngineDatabaseSdkSettings>(
            //    configuration.GetSection(nameof(RulesEngineDatabaseSdkSettings)));
        }
    }
}
