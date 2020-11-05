using System;
using System.Collections.Generic;
using System.Text;

namespace Nebula.CI.Services.Plugin
{
    public class PluginBaseDto
    {
        public string Uid { get; set; }
        public string Name { get; set; }
        public string AnnoName { get; set; }
        public string ConfigUrl { get; set; }
        public string ResultUrl { get; set; }
        public string Description { get; set; }
    }
}
