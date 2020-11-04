using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Domain.Entities;

namespace Nebula.CI.Services.Plugin
{
    public class PluginParam : Entity<Guid>
    {
        public string Name { get; protected set; }
        public string AnnoName { get; protected set; }
        public string Type { get; protected set; }
        public string Description { get; protected set; }
        public string Default { get; protected set; }
        public List<string> Optional { get; protected set; }

        protected PluginParam()
        {
        }
    }
}
