using k8s;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Nebula.CI.Services.Plugin
{
    public class ResourceRepository : BaseRepository, IResourceRepository
    {
        private readonly Kubernetes _client;

        public ResourceRepository(Kubernetes client)
        {
            _client = client;
        }

        public async Task<List<Resource>> GetListAsync()
        {
            //var config = new KubernetesClientConfiguration { Host = "http://172.18.67.167:8001/" };
            //var config = KubernetesClientConfiguration.BuildDefaultConfig();
            //var client = new Kubernetes(config);

            var objs = await _client.ListNamespacedCustomObjectAsync("tekton.dev", "v1alpha1", "ci-nebula", "pipelineresources");

            var str = objs.ToString();
            JObject jo = JObject.Parse(str);
            var itemlist = jo["items"];
            var resources = new List<Resource>();
            foreach (var item in itemlist)
            {
                var name = item["metadata"]["name"].ToString();
                var uid = item["metadata"]["uid"].ToString();
                var annoName = item["metadata"]["annotations"]["name"].ToString();
                var type = item["metadata"]["annotations"]["type"].ToString();

                var resource = CreateEntity<Resource>();
                SetProperty(resource, "Uid", uid);
                SetProperty(resource, "Name", name);
                SetProperty(resource, "AnnoName", annoName);
                SetProperty(resource, "Type", type);
                resources.Add(resource);
            }
            return resources;
        }

    }
}
