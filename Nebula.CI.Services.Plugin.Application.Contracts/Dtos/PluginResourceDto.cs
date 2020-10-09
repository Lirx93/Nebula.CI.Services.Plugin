using System;
using System.Collections.Generic;
using System.Text;

namespace Nebula.CI.Services.Plugin 
{ 
    public class PluginResourceDto
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public bool Optional { get; set; }

    }
}
