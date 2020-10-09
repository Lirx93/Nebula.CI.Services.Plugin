using Microsoft.Extensions.DependencyInjection;
using System;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;

namespace Nebula.CI.Services.Plugin
{
    [DependsOn(typeof(AbpAutoMapperModule))]
    [DependsOn(typeof(PluginEngineModule))]
    public class PluginApplicationModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            var configuration = context.Services.GetConfiguration();

            Configure<AbpAutoMapperOptions>(options =>
            {
                options.AddProfile<PluginApplicationAutoMapperProfile>(validate: true);
            });
        }
    }
}
