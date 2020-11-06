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
            var configuration = context.Services.GetConfiguration();
            var k8sServer = configuration["K8sServer"];

            KubernetesClientConfiguration config;
            if(k8sServer != null && k8sServer != string.Empty)
            {
                config = new KubernetesClientConfiguration { Host = k8sServer };
            }
            else
            {
                config = KubernetesClientConfiguration.BuildDefaultConfig();
            }

            context.Services.AddTransient(typeof(Kubernetes), provider => {
                //var config = new KubernetesClientConfiguration { Host = "http://172.18.67.105:8001/" };
                KubernetesClientConfiguration config = KubernetesClientConfiguration.BuildDefaultConfig();
                return new Kubernetes(config);
            });
        }
    }
}
