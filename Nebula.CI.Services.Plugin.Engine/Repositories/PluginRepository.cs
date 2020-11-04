using k8s;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Nebula.CI.Services.Plugin
{
    public class PluginRepository : BaseRepository, IPluginRepository
    {
        private readonly Kubernetes _client;

        public PluginRepository(Kubernetes client)
        {
            _client = client;
        }

        public async Task<Plugin> GetAsync(string name)
        {
            var objs = await _client.ListNamespacedCustomObjectAsync("tekton.dev", "v1beta1", "ci-nebula", "tasks");

            var str = objs.ToString();
            JObject jo = JObject.Parse(str);
            var itemlist = jo["items"];
            var plugin = CreateEntity<Plugin>();
            foreach (var item in itemlist)
            {
                if (name != item["metadata"]["name"].ToString()) 
                {
                    continue;
                }
                var uid = item["metadata"]["uid"].ToString();
                var annoName = item["metadata"]["annotations"]["name"].ToString();
                var configurl = item["metadata"]["annotations"]["configurl"]?.ToString();
                var resulturl = item["metadata"]["annotations"]["resulturl"]?.ToString();
                
                SetProperty(plugin, "Uid", uid);
                SetProperty(plugin, "Name", name);
                SetProperty(plugin, "AnnoName", annoName);
                if(configurl == null)
                {
                    SetProperty(plugin, "ConfigUrl", "/api/ci/plugins/common/config/");
                }
                else
                {
                    SetProperty(plugin, "ConfigUrl", configurl);
                }

                if(resulturl != null)
                {
                    SetProperty(plugin, "ResultUrl", resulturl);
                }

                var taskParams = GetField<List<PluginParam>>(plugin, "_params");
                var inputResources = GetField<List<PluginResource>>(plugin, "_inputResources");
                var outputResources = GetField<List<PluginResource>>(plugin, "_outputResources");

                foreach (var param in item["spec"]["params"])
                {
                    var taskParam = CreateEntity<PluginParam>(); 
                    SetProperty(taskParam, "Name", param["name"].ToString());
                    SetProperty(taskParam, "Type", param["type"]?.ToString());
                    SetProperty(taskParam, "Default", param["default"]?.ToString()??"");
                    string descstr = param["description"]?.ToString();
                    if(descstr != null)
                    {
                        var descstrlist = descstr.Split("#");
                        SetProperty(taskParam, "AnnoName", descstrlist[0]);
                        if(descstrlist.Length == 2)
                        {
                            SetProperty(taskParam, "Description", descstrlist[1]);
                        }
                        else
                        {
                            SetProperty(taskParam, "Description", "");
                        }
                    }
                    var optional = new List<string>();
                    var optionalstr = item["metadata"]["annotations"][param["name"].ToString()]?.ToString();
                    if(optionalstr != null)
                    {
                        optional = new List<string>(optionalstr.Split("#"));
                    }
                    SetProperty(taskParam, "Optional", optional);
                    taskParams.Add(taskParam);
                }

                try
                {
                    foreach (var input in item["spec"]["resources"]["inputs"])
                    {
                        var annoResources = JsonConvert.DeserializeObject<List<PluginAnnoResource>>(item["metadata"]["annotations"]["resources"].ToString());
                        foreach (var annoResource in annoResources)
                        {
                            if (input["name"].ToString() == annoResource.Name)
                            {
                                var resource = CreateEntity<PluginResource>();
                                SetProperty(resource, "Name", input["name"].ToString());
                                SetProperty(resource, "Type", annoResource.Type);
                                SetProperty(resource, "Description", input["description"]?.ToString());
                                var optional = input["optional"]?.ToString();
                                SetProperty(resource, "Optional", optional != null && bool.Parse(optional));
                                inputResources.Add(resource);
                            }
                        }
                    }
                    foreach (var output in item["spec"]["resources"]["outputs"])
                    {
                        var annoResources = JsonConvert.DeserializeObject<List<PluginAnnoResource>>(item["metadata"]["annotations"]["resources"].ToString());
                        foreach (var annoResource in annoResources)
                        {
                            if (output["name"].ToString() == annoResource.Name)
                            {
                                var resource = CreateEntity<PluginResource>();
                                SetProperty(resource, "Name", output["name"].ToString());
                                SetProperty(resource, "Type", annoResource.Type);
                                SetProperty(resource, "Description", output["description"]?.ToString());
                                var optional = output["optional"]?.ToString();
                                SetProperty(resource, "Optional", optional != null && bool.Parse(optional));
                                outputResources.Add(resource);
                            }
                        }
                    }
                }
                catch (Exception) { }
            }
            return plugin;
        }

        public async Task<List<Plugin>> GetListAsync()
        {
            //var config = new KubernetesClientConfiguration { Host = "http://172.18.67.167:8001/" };
            //var config = KubernetesClientConfiguration.BuildDefaultConfig();
            //var client = new Kubernetes(config);

            var objs = await _client.ListNamespacedCustomObjectAsync("tekton.dev", "v1beta1", "ci-nebula", "tasks");

            var str = objs.ToString();
            JObject jo = JObject.Parse(str);
            var itemlist = jo["items"];
            var plugins = new List<Plugin>();
            foreach (var item in itemlist)
            {
                var name = item["metadata"]["name"].ToString();
                var uid = item["metadata"]["uid"].ToString();
                var annoName = item["metadata"]["annotations"]["name"].ToString();
                var configurl = item["metadata"]["annotations"]["configurl"]?.ToString();
                var resulturl = item["metadata"]["annotations"]["resulturl"]?.ToString();
                           
                var plugin = CreateEntity<Plugin>();
                SetProperty(plugin, "Uid", uid);
                SetProperty(plugin, "Name", name);
                SetProperty(plugin, "AnnoName", annoName);
                if(configurl == null)
                {
                    SetProperty(plugin, "ConfigUrl", "/api/ci/plugins/common/config/");
                }
                else
                {
                    SetProperty(plugin, "ConfigUrl", configurl);
                }

                if(resulturl != null)
                {
                    SetProperty(plugin, "ResultUrl", resulturl);
                }

                var taskParams = GetField<List<PluginParam>>(plugin, "_params");
                var inputResources = GetField<List<PluginResource>>(plugin, "_inputResources");
                var outputResources = GetField<List<PluginResource>>(plugin, "_outputResources");

                foreach (var param in item["spec"]["params"])
                {
                    var taskParam = CreateEntity<PluginParam>();
                    SetProperty(taskParam, "Name", param["name"].ToString());
                    SetProperty(taskParam, "Type", param["type"]?.ToString());
                    SetProperty(taskParam, "Description", param["description"]?.ToString());
                    SetProperty(taskParam, "Default", param["default"]?.ToString());
                    taskParams.Add(taskParam);
                }

                try
                {
                    foreach (var input in item["spec"]["resources"]["inputs"])
                    {
                        var annoResources = JsonConvert.DeserializeObject<List<PluginAnnoResource>>(item["metadata"]["annotations"]["resources"].ToString());
                        foreach (var annoResource in annoResources)
                        {
                            if (input["name"].ToString() == annoResource.Name)
                            {
                                var resource = CreateEntity<PluginResource>();
                                SetProperty(resource, "Name", input["name"].ToString());
                                SetProperty(resource, "Type", annoResource.Type);
                                SetProperty(resource, "Description", input["description"]?.ToString());
                                var optional = input["optional"]?.ToString();
                                SetProperty(resource, "Optional", optional != null && bool.Parse(optional));
                                inputResources.Add(resource);
                            }
                        }
                    }
                    foreach (var output in item["spec"]["resources"]["outputs"])
                    {
                        var annoResources = JsonConvert.DeserializeObject<List<PluginAnnoResource>>(item["metadata"]["annotations"]["resources"].ToString());
                        foreach (var annoResource in annoResources)
                        {
                            if (output["name"].ToString() == annoResource.Name)
                            {
                                var resource = CreateEntity<PluginResource>();
                                SetProperty(resource, "Name", output["name"].ToString());
                                SetProperty(resource, "Type", annoResource.Type);
                                SetProperty(resource, "Description", output["description"]?.ToString());
                                var optional = output["optional"]?.ToString();
                                SetProperty(resource, "Optional", optional != null && bool.Parse(optional));
                                outputResources.Add(resource);
                            }
                        }
                    }
                }
                catch (Exception) { }
                plugins.Add(plugin);
            }
            return plugins;
        }

        public class PluginAnnoResource
        {
            public string Name { get; set; }
            public string Type { get; set; }
        }

    }
}
