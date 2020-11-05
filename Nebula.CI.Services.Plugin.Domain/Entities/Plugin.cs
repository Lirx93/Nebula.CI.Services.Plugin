using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Domain.Entities;

namespace Nebula.CI.Services.Plugin
{
    public class Plugin : AggregateRoot
    {
        public string Uid { get; protected set; }
        public string Name { get; protected set; }
        public string AnnoName { get; protected set; }
        public string ConfigUrl { get; protected set; }
        public string ResultUrl { get; protected set; }
        public string Description { get; protected set; }

        protected List<PluginParam> _params = new List<PluginParam>();
        public IReadOnlyCollection<PluginParam> Params => _params.AsReadOnly();

        protected List<PluginResource> _inputResources = new List<PluginResource>();
        public IReadOnlyCollection<PluginResource> InputResources => _inputResources.AsReadOnly();

        protected List<PluginResource> _outputResources = new List<PluginResource>();
        public IReadOnlyCollection<PluginResource> OutputResources => _outputResources.AsReadOnly();

        protected Plugin()
        {

        }

        public override object[] GetKeys()
        {
            return new object[] { Uid };
        }
    }
}
