using k8s;
using Microsoft.Extensions.DependencyInjection;
using System;
using Volo.Abp.Modularity;

namespace Nebula.CI.Services.Plugin
{
    public class PluginEngineModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddTransient(typeof(Kubernetes), provider => {
                var config = new KubernetesClientConfiguration { Host = "http://172.18.67.167:8001/" };
                //var config = KubernetesClientConfiguration.BuildDefaultConfig();
                return new Kubernetes(config);
            });
        }
    }
}
