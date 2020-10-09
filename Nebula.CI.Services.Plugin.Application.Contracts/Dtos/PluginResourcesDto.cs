using System;
using System.Collections.Generic;
using System.Text;

namespace Nebula.CI.Services.Plugin 
{ 
    public class PluginResourcesDto
    {
        public List<PluginResourceDto> Inputs { get; set; }
        public List<PluginResourceDto> Outputs { get; set; }
    }
}
