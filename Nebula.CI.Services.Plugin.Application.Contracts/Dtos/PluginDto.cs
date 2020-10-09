using System;
using System.Collections.Generic;
using System.Text;

namespace Nebula.CI.Services.Plugin
{
    public class PluginDto
    {
        public string Uid { get; set; }
        public string Name { get; set; }
        public string AnnoName { get; set; }
        public string ConfigUrl { get; set; }
        public string ResultUrl { get; set; } 
        public List<PluginParamDto> Params { get; set; }
        public PluginResourcesDto Resources { get; set; }
    }
}
