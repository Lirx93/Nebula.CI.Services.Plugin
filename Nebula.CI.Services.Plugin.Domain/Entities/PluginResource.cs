using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Domain.Entities;

namespace Nebula.CI.Services.Plugin
{
    public class PluginResource : Entity<Guid>
    {
        public string Name { get; protected set; }
        public string Type { get; protected set; }
        public string Description { get; protected set; }
        public bool Optional { get; protected set; }

        protected PluginResource()
        {
        }
    }
}
